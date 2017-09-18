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
            ImplantInitialPos = Implant.transform.position; //new Vector3(-Implant.transform.position.x, Implant.transform.position.z, -Implant.transform.position.y);
        }
        Dragit = true;
    }
    private void OnMouseDrag() {
        MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
        Vector3 pos = Camera.main.ScreenToWorldPoint(MousePosition);
        if (Dragit) {
            if (Coronal) {
                Vector3 auxPos = ImplantInitialPos + (pos - InitialPos);
                Implant.transform.position = auxPos;
            }
          /*if(Axial)
                this.transform.localPosition = new Vector3(pos.x, this.transform.localPosition.y, -pos.y) - new Vector3(InitialPos.x, 0, -InitialPos.y);
            if(Sagittal)
                this.transform.localPosition = new Vector3(pos.y, pos.x, this.transform.localPosition.y) - new Vector3(InitialPos.y, InitialPos.x, 0); //nope*/
        }
    }
    private void OnMouseUp() {
        Dragit = false;
    }
}
