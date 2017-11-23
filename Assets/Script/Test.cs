﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	private Vector3 PositionDiscrepancy;
	private Vector3 RotationDiscrepancy;

	public Transform Implant;
	public bool DebugOn;
	public GameObject TextObject;
	private string Text;

	private string Path;
	private string TestVariables;

	private bool Start = false;

	private string TimerText;
	private float SecondsCount;
	private int MinuteCount;
	private int HourCount;

	void Update () {
		if (Start) {
			UpdateTimer ();
			Text = TextObject.GetComponent<Text> ().text;
			Path = Application.dataPath + "/Results_" + Text + ".txt";
			if (Text != "") {
				Vector3 auxPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
				PositionDiscrepancy = Implant.transform.localPosition - auxPosition;

				RotationDiscrepancy = Implant.transform.localEulerAngles - transform.localEulerAngles;

				float angleX = RotationDiscrepancy.x;
				angleX = (angleX > 180) ? angleX - 360 : angleX;
				float angleY = RotationDiscrepancy.y;
				angleY = (angleY > 180) ? angleY - 360 : angleY;
				float angleZ = RotationDiscrepancy.z;
				angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;

				RotationDiscrepancy = new Vector3 (angleX, angleY, angleZ);

				if (DebugOn) {
					Debug.Log ("PositionDiscrepancy: " + PositionDiscrepancy.ToString ());
					Debug.Log ("RotationDiscrepancy: " + RotationDiscrepancy.ToString ());
				}

				TestVariables = "User: " + Text + "\r\n" + "Position Discrepancy: " + PositionDiscrepancy.ToString () + "\r\n" + "Rotation Discrepancy: " + RotationDiscrepancy.ToString () + "\r\n" + "Time: " + TimerText;
				System.IO.File.WriteAllText (Path, TestVariables);
			}
		}
	}

	public void UpdateTimer(){
		SecondsCount += Time.deltaTime;
		TimerText = HourCount +"h:"+ MinuteCount +"m:"+(int)SecondsCount + "s";
		if(SecondsCount >= 60){
			MinuteCount++;
			SecondsCount = 0;
		}else if(MinuteCount >= 60){
			HourCount++;
			MinuteCount = 0;
		}    
	} 

	public void StartTest() {
		Start = true;
	}
}
