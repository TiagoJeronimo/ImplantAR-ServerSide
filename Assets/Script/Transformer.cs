﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour {

    private Vector3 LastLocalPosition;
	private Vector3 LastClientLocalPosition;
    public static Vector3 SendingPosition;

	private Vector3 LastLocalRotation;
	private Vector3 LastClientLocalRotation;
    public static Vector3 SendingRotation;

    void Start() {
		LastLocalRotation = Server.LocalRotation;
    }

	void FixedUpdate () {

        Debug.Log("ScaleMode:" + this.transform.lossyScale);
        Debug.Log("Scalelocal:" + this.transform.localScale);

        //Position
        if (LastLocalPosition != this.transform.localPosition) { //this side(Server) changed position
			LastLocalPosition = this.transform.localPosition;
			SendingPosition = new Vector3 (-this.transform.localPosition.x, -this.transform.localPosition.z, -this.transform.localPosition.y);
		} 
		else{
			Vector3 auxServerPosition = new Vector3(-Server.LocalPosition.x, -Server.LocalPosition.z, -Server.LocalPosition.y);
			if(LastClientLocalPosition != auxServerPosition) { //the client change position
				this.transform.localPosition = auxServerPosition;
				LastLocalPosition = auxServerPosition;
				LastClientLocalPosition = auxServerPosition;
			}
		}

        //Rotation
		if(LastLocalRotation != this.transform.localEulerAngles) {
			LastLocalRotation = this.transform.localEulerAngles;
			SendingRotation = new Vector3 (-this.transform.localEulerAngles.x, -this.transform.localEulerAngles.z, -this.transform.localEulerAngles.y);
		} 
		else {
			Vector3 auxServerRotation= new Vector3(-Server.LocalRotation.x, -Server.LocalRotation.z, -Server.LocalRotation.y);
			if(LastClientLocalRotation!= auxServerRotation) { //the client  change position
				this.transform.localEulerAngles = auxServerRotation;
				LastLocalRotation = auxServerRotation;
				LastClientLocalRotation = auxServerRotation;
			}
		}
	}

	/*void OnGUI() {
		GUI.Label(new Rect(10, 40, 1000, 20), "pos: " + this.transform.position);
		GUI.Label(new Rect(10, 60, 1000, 20), "localPos: " + this.transform.localPosition);
	}*/
}
