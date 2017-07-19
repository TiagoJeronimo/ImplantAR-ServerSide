using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour {

    GameObject Jaw;
    Quaternion rotation;

    void Start() {
        rotation = transform.rotation;
        Jaw = GameObject.FindGameObjectWithTag("Jaw");
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Jaw.transform.position - Server.RelativePosition;

        //transform.position = Server.Position;
        transform.rotation = Server.Rotation;
	}

    void LateUpdate() {
        //transform.rotation = rotation;
        //the implant doesn't rotate with his parent because of this. Best option is to change it's rotation on blender 
    }

}
