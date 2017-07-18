using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    // rotate
    public float rotSpeed = 4.0f;

    private void OnMouseDrag() {
        float h = rotSpeed * Input.GetAxis("Mouse X");
        float v = rotSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(-v, 0, h);
    }
}
