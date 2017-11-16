using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

	public float ZoomValue = 30;
    public int ScrollingSensitivity = 2;
    public Camera Camera;

    private float NormalValue;
	private bool IsZoomed = false;

    void Awake () {
        NormalValue = Camera.orthographicSize;
    }

    private void OnMouseOver() {
        ZoomInOut();
    }

    void ZoomInOut()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && (Input.GetAxis("Mouse ScrollWheel") + Camera.orthographicSize) > ZoomValue + ScrollingSensitivity)
        {
            for (int sensitivityOfScrolling = ScrollingSensitivity; sensitivityOfScrolling > 0; sensitivityOfScrolling--) Camera.orthographicSize--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && (Input.GetAxis("Mouse ScrollWheel") + Camera.orthographicSize) < NormalValue - ScrollingSensitivity)
        {
            for (int sensitivityOfScrolling = ScrollingSensitivity; sensitivityOfScrolling > 0; sensitivityOfScrolling--) Camera.orthographicSize++;
        }
    }
}
