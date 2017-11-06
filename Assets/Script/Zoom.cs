using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

	public float ZoomValue = 45;
	public float NormalValue = 90;
    public float Smooth = 5;

    public Camera Camera;

	private bool IsZoomed = false;

	void Update () {
		if (IsZoomed) {
            Camera.orthographicSize = Mathf.Lerp (Camera.orthographicSize, ZoomValue, Time.deltaTime * Smooth);
		} else {
            Camera.orthographicSize = Mathf.Lerp (Camera.orthographicSize, NormalValue, Time.deltaTime * Smooth);
        }
	}

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(2)) {
            IsZoomed = !IsZoomed;
        }
    }
}
