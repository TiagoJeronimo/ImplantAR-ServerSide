using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

	public Vector3 ZoomValue = new Vector3(4.6f,4.6f,1);
	public Vector3 NormalValue = new Vector3(2.3f, 2.3f, 1);
    public float Smooth = 5;

	private bool IsZoomed = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			IsZoomed = !IsZoomed;
		}

		if (IsZoomed) {
			this.transform.localScale = Vector3.Lerp (this.transform.localScale, ZoomValue, Time.deltaTime * Smooth);
		} else {
            this.transform.localScale = Vector3.Lerp (this.transform.localScale, NormalValue, Time.deltaTime * Smooth);
        }
	}
}
