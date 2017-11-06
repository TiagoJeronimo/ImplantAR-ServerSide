using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform3DView : MonoBehaviour {

    //Zoom
    private float ZoomAmount = 0;
    public float MaxToZoom = 1;
    public float ScrollSpeed = 10;

    //Rot
    public float RotSpeed = 6.0f;
    public static bool IsRotating;

    //Pan
    public float PanVelocity = 5.0f;
    private Vector3 InitialPos;
    private Vector3 PanOrigin;

    public Camera Camera;

    void Update() {

        if (Input.GetMouseButtonUp(1)) {
            IsRotating = false;
        }

        if (IsRotating) {
            transform.GetChild(0).Rotate((Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime), (Input.GetAxis("Mouse X") * -RotSpeed * Time.deltaTime), 0, Space.World);
        }

    }

    private void OnMouseOver() {
        Debug.Log("over");
        if (Input.GetMouseButtonDown(1)) {
            IsRotating = true;
        }

                //Zoom
        ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToZoom, MaxToZoom);
        float translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToZoom - Mathf.Abs(ZoomAmount));
        Camera.transform.Translate(0, 0, translate * ScrollSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            InitialPos = Camera.transform.localPosition;
            PanOrigin = Camera.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void OnMouseDrag() {
        if (Input.GetMouseButton(0)) {
            Vector3 pos = Camera.ScreenToViewportPoint(Input.mousePosition) - PanOrigin;
            Camera.transform.localPosition = InitialPos - pos * PanVelocity;
        }
    }
}
