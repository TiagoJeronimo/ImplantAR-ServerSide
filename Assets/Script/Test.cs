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

    private int LastAttachScrew;
    private int AttemptNumber = 0;

    private string[] TestResult = new string[10];

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
					//Debug.Log ("PositionDiscrepancy: " + PositionDiscrepancy.ToString ());
					//Debug.Log ("RotationDiscrepancy: " + RotationDiscrepancy.ToString ());
				}

                TestVariables = "Attempt number: " + AttemptNumber + "\r\n" + "Position Discrepancy: " + PositionDiscrepancy.ToString () + "\r\n" + "Rotation Discrepancy: " + RotationDiscrepancy.ToString () + "\r\n" + "Time: " + TimerText;
                TestResult[AttemptNumber] = TestVariables;

                if (LastAttachScrew != Server.AttachScrew) {
                    LastAttachScrew = Server.AttachScrew;
                    if (Server.AttachScrew == 1) {  
                        Debug.Log("AttemptNumber: " + AttemptNumber);
                        AttemptNumber++;
                    }
                }
                EndTest();
            }
		}
	}

    private void EndTest()
    {
        System.IO.File.WriteAllText(Path, TestResult[0] + "\r\n" + "\r\n" +TestResult[1] + "\r\n" + "\r\n" + TestResult[2] + "\r\n" + "\r\n" + TestResult[3] + "\r\n" + "\r\n" + TestResult[4] + "\r\n" + "\r\n" + TestResult[5] + "\r\n" + "\r\n" + TestResult[6] + "\r\n" + "\r\n" + TestResult[7] + "\r\n" + "\r\n" + TestResult[8]);
    }

	private void UpdateTimer(){
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
