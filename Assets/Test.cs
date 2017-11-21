using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	private Vector3 PositionDiscrepancy;
	private Vector3 RotationDiscrepancy;

	public Transform Implant;
	public bool DebugOn;

	void Update () {
		Vector3 auxPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 5.34f);
		PositionDiscrepancy = Implant.transform.localPosition - auxPosition;
		RotationDiscrepancy = Implant.transform.localEulerAngles - transform.localEulerAngles;

		if (DebugOn) {
			//Debug.Log ("PositionDiscrepancy: " + PositionDiscrepancy);
			//Debug.Log ("RotationDiscrepancy: " + RotationDiscrepancy);
		}
	}
}
