using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Parabox.CSG;

public class pb_CGS : MonoBehaviour {

    GameObject cube;
    GameObject sphere;
    GameObject composite;
    public Material myMaterial;
    public GameObject publicCube;
    public GameObject publicSphere;
    Vector3 LastPos;

    void Start() {
        // Initialize two new meshes in the scene
        /*cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = Vector3.one * 1.3f;*/

        // Perform boolean operation
        //Mesh m = CSG.Subtract(cube, sphere);

        //publicSphere.transform.localScale = Vector3.one * 1.3f;
        LastPos = publicCube.transform.position;

    }

    void Update() {
        if (LastPos != publicCube.transform.position) {
            LastPos = publicCube.transform.position;

            Mesh m = CSG.Intersect(publicCube, publicSphere);

            // Create a gameObject to render the result
            Destroy(composite);
            composite = new GameObject();
            //composite.transform.localPosition = transform.parent.position;
            composite.transform.SetParent(this.transform.parent);
            /*composite.transform.localPosition = new Vector3(2.4f,-0.9f,-2.1f);
            composite.transform.localRotation = Quaternion.Euler(90,0,-180);
            composite.transform.localScale = new Vector3(0.02f,0.02f,0.02f);*/
            composite.AddComponent<MeshFilter>().sharedMesh = m;
            composite.AddComponent<MeshRenderer>().sharedMaterial = myMaterial;
        }
    }
}
