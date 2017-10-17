Shader "Unlit/MRI"
{
	Properties
	{
		_InnerColor("Color", Color) = (0,0,0,1)
		_HiddenColor("Hidden color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"


			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 viewN : TEXCOORD0;
			};

			uniform float4 _HiddenColor, _InnerColor;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;
				o.viewN = normalize(mul(UNITY_MATRIX_IT_MV, o.normal.xyzz));
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return lerp(_HiddenColor, _InnerColor, step(dot(float3(0,0,1), i.viewN) + .08, 0));
			}
			ENDCG
		}
	}
}
