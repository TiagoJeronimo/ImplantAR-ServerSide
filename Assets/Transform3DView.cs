using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform3DView : MonoBehaviour {

    //Zoom
    public float ZoomValue = 10;
    public float Smooth = 5;
    private bool IsZoomed = false;
    private float ZCameraInitialPos;
    private float ZTransformPos;

    //Rot
    public float RotSpeed = 6.0f;
    public static bool IsRotating;

    //Pan
    public float PanVelocity = 5.0f;
    private Vector3 InitialPos;
    private Vector3 PanOrigin;

    public Camera Camera;

    void Start() {
        ZCameraInitialPos = Camera.transform.localPosition.z;
        ZTransformPos = Camera.transform.localPosition.z + ZoomValue;
    }

    void Update() {

        if (IsZoomed) {
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, Mathf.Lerp(Camera.transform.localPosition.z, ZTransformPos, Time.deltaTime * Smooth));
        } else {
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y, Mathf.Lerp(Camera.transform.localPosition.z, ZCameraInitialPos, Time.deltaTime * Smooth));
        }

        if (Input.GetMouseButtonUp(1)) {
            IsRotating = false;
        }

        if (IsRotating) {
            transform.Rotate((Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime), (Input.GetAxis("Mouse X") * -RotSpeed * Time.deltaTime), 0, Space.World);
        }

    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            IsRotating = true;
        }

        if (Input.GetMouseButtonDown(2)) {
            IsZoomed = !IsZoomed;
        }
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
