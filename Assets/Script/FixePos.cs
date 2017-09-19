using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixePos : MonoBehaviour {

    public Transform Implant;
    public Transform Plan;

    public bool Axial;
    public bool Sagittal;
    public bool Coronal;

    Vector3 ImplantInitialPos;

    private Vector3 MousePosition;
    private Vector3 InitialPos;
    public static bool Dragit = false;
    float DistZ = 0;
	
	void FixedUpdate () {
        if (Axial) this.transform.localPosition = new Vector3(-Implant.transform.position.x, this.transform.localPosition.y, -Implant.transform.position.z); //AXIAL
        if (Sagittal) this.transform.localPosition = new Vector3(this.transform.localPosition.x, -Implant.transform.position.y, -Implant.transform.position.z); //SAGITTAL
        if (Coronal) this.transform.localPosition = new Vector3(-Implant.transform.position.x, -Implant.transform.position.y, this.transform.localPosition.z); //CORONAL
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
            InitialPos = Camera.main.ScreenToWorldPoint(MousePosition);
            ImplantInitialPos = Implant.transform.position;
        }
        Dragit = true;
    }
    private void OnMouseDrag() {
        MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
        
        if (Dragit) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(MousePosition);
            Vector3 auxPos = pos - InitialPos;
            if (Coronal) {
                Implant.transform.position = ImplantInitialPos + auxPos;
            }
            else if (Axial) {
                Vector3 auxTransf = new Vector3(ImplantInitialPos.x + auxPos.x, ImplantInitialPos.y, ImplantInitialPos.z - auxPos.y);
                Implant.transform.position = new Vector3(auxTransf.x, Implant.transform.position.y, auxTransf.z);
            } 
            else if (Sagittal) {
                Vector3 auxTransf = new Vector3(ImplantInitialPos.x, ImplantInitialPos.y + auxPos.y, ImplantInitialPos.z + auxPos.x);
                Implant.transform.position = new Vector3(Implant.transform.position.x, auxTransf.y, auxTransf.z);
            }
        }
    }
    private void OnMouseUp() {
        Dragit = false;
    }
}
