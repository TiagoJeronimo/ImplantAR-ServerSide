using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour {

    Vector3 LastPosition;
    Vector3 LastLocalPosition;

	Vector3 LastRotation;

    Vector3 LastClientRelativePos;

    public static Vector3 PositionRelativeToJaw;

    void Start() {
		LastRotation = Server.Rotation;
    }

	void FixedUpdate () {

		if (LastClientRelativePos == Server.RelativePosition) {
			if (LastPosition != this.transform.position) {
				LastLocalPosition = this.transform.localPosition;
				PositionRelativeToJaw = this.transform.localPosition;
			} else {
				if (LastPosition == this.transform.position) {
					this.transform.localPosition = LastLocalPosition;
				} else if (LastPosition != this.transform.position) {
					LastPosition = this.transform.position;
					LastLocalPosition = this.transform.localPosition;
				}
			}
		} else if (!Rotate.IsRotating && LastClientRelativePos != Server.RelativePosition) {
			LastClientRelativePos = Server.RelativePosition;
			this.transform.localPosition = Server.RelativePosition;
			if (LastPosition == this.transform.position) {
				this.transform.localPosition = LastLocalPosition;
			} else if (LastPosition != this.transform.position) {
				LastPosition = this.transform.position;
				LastLocalPosition = this.transform.localPosition;
			}   
		}

		if (LastRotation != Server.Rotation) {
			LastRotation = Server.Rotation;
			transform.localEulerAngles = new Vector3 (-Server.Rotation.x, -Server.Rotation.z, Server.Rotation.y);
		}
	}

	/*void OnGUI() {
		GUI.Label(new Rect(10, 40, 1000, 20), "pos: " + this.transform.position);
		GUI.Label(new Rect(10, 60, 1000, 20), "localPos: " + this.transform.localPosition);
	}*/
}
