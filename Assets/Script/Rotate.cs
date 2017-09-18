using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    // rotate
    public float RotSpeed = 4.0f;
    public static bool IsRotating;

    private void OnMouseDown() {
        IsRotating = true;
    }

    private void OnMouseDrag() {

        float rotX = Input.GetAxis("Mouse X") * RotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * RotSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX, Space.Self);
        transform.Rotate(Vector3.right, rotY, Space.Self);
    }

    private void OnMouseUp() {
        IsRotating = false;
    }
}
