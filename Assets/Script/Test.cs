using System.Collections;
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

	void Update () {
		Text = TextObject.GetComponent<Text>().text;
		Path = Application.dataPath + "/Results" + Text + ".txt";
		if (Text != "") {
			Vector3 auxPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 5.34f);
			PositionDiscrepancy = Implant.transform.localPosition - auxPosition;

			RotationDiscrepancy = Implant.transform.localEulerAngles - transform.localEulerAngles;

			float angleX = RotationDiscrepancy.x + 90;
			angleX = (angleX > 180) ? angleX - 360 : angleX;
			float angleY = RotationDiscrepancy.y;
			angleY = (angleY > 180) ? angleY - 360 : angleY;
			float angleZ = RotationDiscrepancy.z;
			angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;

			RotationDiscrepancy = new Vector3 (angleX,angleY,angleZ);

			if (DebugOn) {
				Debug.Log ("PositionDiscrepancy: " + PositionDiscrepancy.ToString());
				Debug.Log ("RotationDiscrepancy: " + RotationDiscrepancy.ToString());
			}

			TestVariables = "User: " + Text + "\r\n" + "Position Discrepancy: " + PositionDiscrepancy.ToString () + "\r\n" + "Rotation Discrepancy: " + RotationDiscrepancy.ToString ();
			System.IO.File.WriteAllText (Path, TestVariables);
		}
	}
}
