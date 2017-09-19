using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour {

    private Vector3 MousePosition;
    private Vector3 InitialPos;
    public static bool Dragit;
    float DistZ = 0;

    private bool CanPan;

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            if (hit.transform.CompareTag("MeshLess")) {
                CanPan = false;
            } 
        } else {
            CanPan = true;
        }

    }

    private void OnMouseDown() {
        if (CanPan) {
            if (Input.GetMouseButtonDown(0)) {
                MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
                InitialPos = Camera.main.ScreenToWorldPoint(MousePosition) - transform.position;
            }
            Dragit = true;
        }
    }
    private void OnMouseDrag() {
        if (CanPan) {
            MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
            Vector3 pos = Camera.main.ScreenToWorldPoint(MousePosition);
            if (Dragit)
                transform.position = pos - InitialPos;
        }
    }
    private void OnMouseUp() {
        if (CanPan) {
            Dragit = false;
        }
    }
}
