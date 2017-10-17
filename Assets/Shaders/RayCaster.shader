// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// shader that performs ray casting using a 3D texture
// adapted from a Cg example by Nvidia
// http://developer.download.nvidia.com/SDK/10/opengl/samples.html
// Gilles Ferrand, University of Manitoba 2016

Shader "Custom/Ray Casting" {
	
	Properties {
		// the data cube
		[NoScaleOffset] _Data ("Data Texture", 3D) = "" {}
	}

	SubShader {
		Tags {"Queue"="Transparent"}
		Pass{
			ColorMask 0
		}
	    Pass {
	        ZWrite Off
	        ColorMask RGB
	        Blend SrcAlpha OneMinusSrcAlpha
			Cull Off

			CGPROGRAM
	        #pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler3D _Data;
			float _Opacity;
			int _Iterations;
			float _Alpha;
			float _SliceAxis1Min, _SliceAxis1Max;
			float _SliceAxis2Min, _SliceAxis2Max;
			float _SliceAxis3Min, _SliceAxis3Max;
			float _DataMin, _DataMax;
			float _Normalization;

			// calculates intersection between a ray and a box
			// http://www.siggraph.org/education/materials/HyperGraph/raytrace/rtinter3.htm
			bool IntersectBox(float3 ray_o, float3 ray_d, float3 boxMin, float3 boxMax, out float tNear, out float tFar)
			{
			    // compute intersection of ray with all six bbox planes
			    float3 invR = 1.0 / ray_d;
			    float3 tBot = invR * (boxMin.xyz - ray_o);
			    float3 tTop = invR * (boxMax.xyz - ray_o);
			    // re-order intersections to find smallest and largest on each axis
			    float3 tMin = min (tTop, tBot);
			    float3 tMax = max (tTop, tBot);
			    // find the largest tMin and the smallest tMax
			    float2 t0 = max (tMin.xx, tMin.yz);
			    float largest_tMin = max (t0.x, t0.y);
			    t0 = min (tMax.xx, tMax.yz);
			    float smallest_tMax = min (t0.x, t0.y);
			    // check for hit
			    bool hit = (largest_tMin <= smallest_tMax);
			    tNear = largest_tMin;
			    tFar = smallest_tMax;
			    return hit;
			}

			struct frag_input {
			    float4 pos : SV_POSITION;
			    float2 uv : TEXCOORD0;
			    float3 ray_o : TEXCOORD1; // ray origin
			    float3 ray_d : TEXCOORD2; // ray direction
			};

			// vertex program
			frag_input vert(appdata_img i)
			{
				frag_input o;

			    // calculate eye ray in object space
				o.ray_d = -ObjSpaceViewDir(i.vertex);
				o.ray_o = i.vertex.xyz - o.ray_d;
				// calculate position on screen (unused)
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uv = i.texcoord.xy;

				return o;
			}

			// gets data value at a given position
			float4 get_data(float3 pos) {
				// sample texture (pos is normalized in [0,1])
				float3 pos_righthanded = float3(pos.x,pos.y,pos.z);
				//float data = tex3D(_Data, pos_righthanded).a;
				float data = tex3Dlod(_Data, float4(pos_righthanded,0)).a;
				// slice and threshold
				data *= step(_SliceAxis1Min, pos.x);
				data *= step(_SliceAxis2Min, pos.y);
				data *= step(_SliceAxis3Min, pos.z);
				data *= step(pos.x, _SliceAxis1Max);
				data *= step(pos.y, _SliceAxis2Max);
				data *= step(pos.z, _SliceAxis3Max);
				data *= step(_DataMin, data);
				data *= step(data, _DataMax);
				// colourize
				//For MRI image files and raw files
				//float4 data4 = tex3Dlod(_Data, float4(pos_righthanded,0));
				//data4.a = data;
				//return data4;

				//For raw files that only contains alpha information
				float4 col = float4(data, data, data, data);
				return col;
			}

#define STEP_CNT _Iterations // should ideally be at least as large as data resolution, but strongly affects frame rate
			
			// fragment program
			float4 frag(frag_input i) : COLOR
			{
			    i.ray_d = normalize(i.ray_d);
			    // calculate eye ray intersection with cube bounding box
				float3 boxMin = { -0.5, -0.5, -0.5 };
				float3 boxMax = {  0.5,  0.5,  0.5 };
			    float tNear, tFar;
			    bool hit = IntersectBox(i.ray_o, i.ray_d, boxMin, boxMax, tNear, tFar);

			    if (!hit) discard;
			    if (tNear < 0.0) tNear = 0.0;

			    // calculate intersection points
			    float3 pNear = i.ray_o + i.ray_d*tNear;
			    float3 pFar  = i.ray_o + i.ray_d*tFar;

			    // convert to texture space
				pNear += 0.5;
				pFar  += 0.5;

			    // march along ray inside the cube, accumulating color
				float3 ray_pos = pNear;
				float3 ray_dir = pFar - pNear;

				float3 ray_step = normalize(ray_dir) * sqrt(3) / STEP_CNT;
				float4 ray_col = 0;

				for(int k = 0; k < STEP_CNT; k++)
				{
					float4 voxel_col = get_data(ray_pos);
					voxel_col.a *= saturate(_Opacity);
					 
					ray_col.rgb = ray_col.rgb + (1 - ray_col.a) * voxel_col.a * voxel_col.rgb;
					ray_col.a   = ray_col.a   + (1 - ray_col.a) * voxel_col.a;

					ray_pos += ray_step;

					if (ray_pos.x < 0 || ray_pos.y < 0 || ray_pos.z < 0) break;
					if (ray_pos.x > 1 || ray_pos.y > 1 || ray_pos.z > 1) break;

				}

				ray_col.a = (ray_col.r + ray_col.g + ray_col.b)/3;

				if(ray_col.r <= _Alpha && ray_col.g <= _Alpha && ray_col.b <= _Alpha)
					ray_col.a = 0.0f;

				half4 dst = ray_col * _Normalization;

		    	return dst;

			}

			ENDCG

		}

	}

	FallBack Off
}