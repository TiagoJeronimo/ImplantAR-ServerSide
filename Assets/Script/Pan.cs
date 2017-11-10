using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pan : MonoBehaviour {

    public Camera Camera;

    private Vector3 MousePosition;
    private Vector3 InitialPos;
    private bool Dragit;
    private float DistZ = 0;

    private bool IsSliding;
    private bool CanPan = true;

    void Update() {
        if (!IsSliding && CanPan) {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform.CompareTag("MeshLess")) {
                        CanPan = false;
                    } else {
                        CanPan = true;
                    }
                }
            }
        } 
    }

    public void SliderSelected() {
        IsSliding = true;
    }

    public void SliderDeselected() {
        IsSliding = false;
    }

    private void OnMouseDown() {
        if (!IsSliding) {
            if (CanPan) {
                if (Input.GetMouseButtonDown(0)) {
                    MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
                    InitialPos = Camera.ScreenToWorldPoint(MousePosition) - transform.position;
                }
                Dragit = true;
            }
        }
    }
    private void OnMouseDrag() {
        if (!IsSliding) {
            if (CanPan) {
                MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
                Vector3 pos = Camera.ScreenToWorldPoint(MousePosition);
                if (Dragit)
                    transform.position = pos - InitialPos;
            }
        }
    }
    private void OnMouseUp() {
        Dragit = false;
        CanPan = true;
    }
}
