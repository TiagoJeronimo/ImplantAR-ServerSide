using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour {

    GameObject Jaw;
    Quaternion rotation;

    Vector3 LastPosition;
    Vector3 LastLocalPosition;

    void Start() {
        rotation = transform.rotation;
        Jaw = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Rotate.IsRotating) {
            /*this.transform.position = Jaw.transform.position - Server.RelativePosition;
            if(LastPosition == this.transform.position) {
                this.transform.localPosition = LastLocalPosition;
            } 
            else if (LastPosition != this.transform.position) {
                //Debug.Log("entrei"); //continua a entrar aqui!!!
                LastPosition = this.transform.position;
                LastLocalPosition = this.transform.localPosition;
            }*/
            
            //Debug.Log("- position: " + this.transform.position);
            //Debug.Log("- localPosition: " + this.transform.localPosition);
        }

        //transform.rotation = Server.Rotation;
	}
}
