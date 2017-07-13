using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    // rotate
    public float rotSpeed = 4.0f;
    bool isRotating;
    Vector3 rotationAxisX;
    Vector3 rotationAxisY;
    Vector3 mouseOrigin;
    Vector3 angleDelta;
    GameObject rotationCentre;

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 500.0f)) {
                //Debug.Log("You selected the " + hit.transform.name);
                isRotating = true;
                if (Input.GetMouseButtonDown(0)) rotationCentre = this.gameObject;
                if (Input.GetMouseButtonDown(1)) rotationCentre = Camera.main.gameObject;
                mouseOrigin = Input.mousePosition;
            }
        }

        if (isRotating) {
            rotationAxisX = Camera.main.transform.up;
            rotationAxisY = Camera.main.transform.right;
            angleDelta = (Input.mousePosition - mouseOrigin) / Screen.width;
            angleDelta *= rotSpeed;
            angleDelta.x *= -1;
            this.transform.RotateAround(rotationCentre.transform.position, rotationAxisX, angleDelta.x);
            this.transform.RotateAround(rotationCentre.transform.position, rotationAxisY, angleDelta.y);
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) isRotating = false;
        }
    }
}
