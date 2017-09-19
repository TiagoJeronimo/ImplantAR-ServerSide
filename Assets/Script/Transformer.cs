using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour {

    GameObject Jaw;
    Quaternion rotation;

    Vector3 LastPosition;
    Vector3 LastLocalPosition;

    Vector3 LastClientRelativePos;

    void Start() {
        rotation = transform.rotation;
        Jaw = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       if (!Rotate.IsRotating) {
            Debug.Log("aqui");
            LastClientRelativePos = Server.RelativePosition;
            this.transform.position = Jaw.transform.position - Server.RelativePosition;
            if(LastPosition == this.transform.position) {
                this.transform.localPosition = LastLocalPosition;
            } 
            else if (LastPosition != this.transform.position) {
                //Debug.Log("entrei"); //continua a entrar aqui!!!
                LastPosition = this.transform.position;
                LastLocalPosition = this.transform.localPosition;
            }   
            //Debug.Log("- position: " + this.transform.position);
            //Debug.Log("- localPosition: " + this.transform.localPosition);
        }
        //transform.rotation = Server.Rotation;
	}
}
