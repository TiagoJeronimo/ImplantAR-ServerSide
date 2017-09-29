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
    }

	//PROBLEM: CAN ONLY USE LOCALPOSITION
	void FixedUpdate () {
		if (LastClientRelativePos == Server.RelativePosition) {
			if (LastPosition != this.transform.position) {
				LastLocalPosition = this.transform.localPosition;
				PositionRelativeToJaw = Jaw.transform.position - this.transform.position;
			} else {
				if (LastPosition == this.transform.position) {
					this.transform.localPosition = LastLocalPosition;
				} else if (LastPosition != this.transform.position) {
					LastPosition = this.transform.position;
					LastLocalPosition = this.transform.localPosition;
				}
			}
		} 
		else if (!Rotate.IsRotating && LastClientRelativePos != Server.RelativePosition) {
			LastClientRelativePos = Server.RelativePosition;
			this.transform.localPosition = Jaw.transform.position - Server.RelativePosition;
            if(LastPosition == this.transform.position) {
                this.transform.localPosition = LastLocalPosition;
            } 
            else if (LastPosition != this.transform.position) {
                LastPosition = this.transform.position;
                LastLocalPosition = this.transform.localPosition;
            }   
        }
        //transform.rotation = Server.Rotation;
	}
}
