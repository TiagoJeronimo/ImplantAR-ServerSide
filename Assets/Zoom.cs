using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

	public float ZoomValue = 10;
	public float NormalValue = 60;
	public float Smooth = 5;

	private bool IsZoomed = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			IsZoomed = !IsZoomed;
		}

		if (IsZoomed) {
			GetComponent<Camera> ().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, ZoomValue, Time.deltaTime * Smooth);
		} else {
			GetComponent<Camera> ().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, NormalValue, Time.deltaTime * Smooth);
		}
	}
}
