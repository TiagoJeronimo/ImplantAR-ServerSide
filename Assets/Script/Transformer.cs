using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour {

    GameObject Jaw;
    Quaternion rotation;

    Vector3 LastPosition;
    Vector3 LastLocalPosition;

    Vector3 LastClientRelativePos;

    public static Vector3 PositionRelativeToJaw;

    void Start() {
        rotation = transform.rotation;
        Jaw = transform.parent.gameObject;

        //InvokeRepeating("UpdateTransform", 2f, 2f);
    }
	
	// Update is called once per frame
	void Update () {

        if(LastPosition != this.transform.position) {
            LastLocalPosition = this.transform.position;
            PositionRelativeToJaw = Jaw.transform.position - this.transform.position;
        } else {
            this.transform.position = Jaw.transform.position - Server.RelativePosition;
            if (LastPosition == this.transform.position) {
                this.transform.localPosition = LastLocalPosition;
            } else if (LastPosition != this.transform.position) {
                LastPosition = this.transform.position;
                LastLocalPosition = this.transform.localPosition;
            }
        }



      /* if (!Rotate.IsRotating && LastClientRelativePos != Server.RelativePosition) {
            Debug.Log("Client mudou de pos");
            LastClientRelativePos = Server.RelativePosition;
            this.transform.position = Jaw.transform.position - Server.RelativePosition;
            if(LastPosition == this.transform.position) {
                this.transform.localPosition = LastLocalPosition;
            } 
            else if (LastPosition != this.transform.position) {
                LastPosition = this.transform.position;
                LastLocalPosition = this.transform.localPosition;
            }   
            //Debug.Log("- position: " + this.transform.position);
            //Debug.Log("- localPosition: " + this.transform.localPosition);
        }
        //transform.rotation = Server.Rotation;*/
	}
}
