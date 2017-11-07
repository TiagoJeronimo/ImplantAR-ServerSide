using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStencilImplant : MonoBehaviour {

    public Camera Camera;
	public Transform Implant;
	public bool Axial;
	public bool Sagittal;
	public bool Coronal;

    //Position
	private Vector3 ImplantInitialPos;
	private Vector3 MousePosition;
	private Vector3 InitialPos;
	private bool Dragit = false;
    private bool IsSliding;

    //Rotation
    private Vector3 ImplantInitialRotation;
    private bool Rotating = false;

    void Update() {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!IsSliding) {
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("MeshLess")) {
               if (Input.GetMouseButtonDown(0) && !Dragit) {
                    MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                    InitialPos = Camera.ScreenToWorldPoint(MousePosition);
                    ImplantInitialPos = Implant.transform.localPosition;
                    Dragit = true;
                }
                else if (Input.GetMouseButtonDown(1) && !Rotating) {
                    MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                    InitialPos = Camera.ScreenToWorldPoint(MousePosition);
                    ImplantInitialRotation = Implant.transform.localEulerAngles;
                    Rotating = true;
                }
            }

            MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            if (Dragit) {
                Vector3 pos = Camera.ScreenToWorldPoint(MousePosition);
                Vector3 auxPos = pos - InitialPos;
                if (Coronal) {
                    Vector3 auxTransf = new Vector3(ImplantInitialPos.x + auxPos.x, Implant.transform.localPosition.y, ImplantInitialPos.z - auxPos.y);
                    Implant.transform.localPosition = new Vector3(auxTransf.x, Implant.transform.localPosition.y, auxTransf.z);
                } else if (Axial) {
                    Vector3 auxTransf = new Vector3(ImplantInitialPos.x + auxPos.x, ImplantInitialPos.y + auxPos.y, Implant.transform.localPosition.z);
                    Implant.transform.localPosition = new Vector3(auxTransf.x, auxTransf.y, Implant.transform.localPosition.z);
                } else if (Sagittal) {
                    Vector3 auxTransf = new Vector3(Implant.transform.localPosition.x, ImplantInitialPos.y - auxPos.x, ImplantInitialPos.z - auxPos.y);
                    Implant.transform.localPosition = new Vector3(Implant.transform.localPosition.x, auxTransf.y, auxTransf.z);
                }
            } else if (Rotating) {
                Vector3 pos = Camera.ScreenToWorldPoint(MousePosition);
                Vector3 auxPos = pos - InitialPos;
                if (Coronal) {
                    Implant.transform.localEulerAngles = new Vector3(Implant.transform.localEulerAngles.x, ImplantInitialRotation.y - auxPos.x, Implant.transform.localEulerAngles.z);
                } else if (Sagittal) {
                    Implant.transform.localEulerAngles = new Vector3(ImplantInitialRotation.x - auxPos.x, Implant.transform.localEulerAngles.y, Implant.transform.localEulerAngles.z);
                }
            }

            if (Input.GetMouseButtonUp(0))
                Dragit = false;
            if (Input.GetMouseButtonUp(1))
                Rotating = false;
        }
    }

    public void SliderSelected() {
        IsSliding = true;
    }

    public void SliderDeselected() {
        IsSliding = false;
    }

}
