using RuntimeGizmos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform3DView : MonoBehaviour {

    public float ZoomValue = 10;
    public int ScrollingSensitivity = 2;
    private bool IsZoomed = false;

    //Rot
    public float RotSpeed = 6.0f;
    public static bool IsRotating;

    //Pan
    public float PanVelocity = 5.0f;
    private Vector3 InitialPos;
    private Vector3 PanOrigin;

    public Camera Camera;

    public static bool MakeTransformations;

    void Update() {

        if (TransformGizmo.target == null)
        {
            if (Input.GetMouseButtonUp(1))
            {
                IsRotating = false;
            }

            if (IsRotating)
            {
                transform.GetChild(0).Rotate((Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime), (Input.GetAxis("Mouse X") * -RotSpeed * Time.deltaTime), 0, Space.World);
            }
        } else
        {
            IsRotating = false;
        }

    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1))
        {
            IsRotating = true;
        }
        if (Input.GetMouseButtonDown(2))
        {
            IsZoomed = !IsZoomed;
        }

        ZoomInOut();
    }

    void ZoomInOut()
    {
        float fov  = Camera.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * ScrollingSensitivity;
        fov = Mathf.Clamp(fov, ZoomValue, 60);
        Camera.fieldOfView = fov;
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0))
        {
            InitialPos = Camera.transform.localPosition;
            PanOrigin = Camera.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void OnMouseDrag() {
        if (TransformGizmo.target == null)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Camera.ScreenToViewportPoint(Input.mousePosition) - PanOrigin;
                Camera.transform.localPosition = InitialPos - pos * PanVelocity;
            }
        }
    }
}