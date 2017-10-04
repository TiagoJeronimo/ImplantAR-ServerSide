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

		transform.Rotate((Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime), (Input.GetAxis("Mouse X") * -RotSpeed * Time.deltaTime), 0, Space.World);
	
    }

    private void OnMouseUp() {
        IsRotating = false;
    }
}
