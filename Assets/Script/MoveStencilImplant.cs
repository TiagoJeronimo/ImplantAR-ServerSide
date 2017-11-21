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
	private Vector3 OldMousePosition; 
	private Vector3 InitialPos;
	private bool Dragit = false;
    private bool IsSliding;

    //Rotation
    private Vector3 ImplantInitialRotation;
    private bool Rotating = false;

	//Used to prevent moving the widget implant in two directions at the same click
	private bool Getdir = true;
	private int DirNumb = 5;
	public bool MouseOneDirection = true;

	public float WidImpDif = 2;

	void Start() {
		OldMousePosition = MousePosition;
	}

    void Update() {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!IsSliding) {
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("MeshLess")) {
               if (Input.GetMouseButtonDown(0) && !Dragit && !Rotating) {
                    MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                    InitialPos = Camera.ScreenToWorldPoint(MousePosition);
                    ImplantInitialPos = Implant.transform.localPosition;
                    Dragit = true;
                }
                else if (Input.GetMouseButtonDown(1) && !Rotating && !Dragit) {
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

				if (MouseOneDirection) {
					if (Getdir && (Mathf.Abs (auxPos.x) >= 1 || Mathf.Abs (auxPos.y) >= 1))
						DirNumb = MouseDirection (auxPos);

					if (DirNumb == 0)
						auxPos.y = 0;
					else if (DirNumb == 1)
						auxPos.x = 0;
				}

                if (Coronal) {
                    Vector3 auxTransf = new Vector3(ImplantInitialPos.x + auxPos.x/WidImpDif, Implant.transform.localPosition.y, ImplantInitialPos.z - auxPos.y/WidImpDif);
                    Implant.transform.localPosition = new Vector3(auxTransf.x, Implant.transform.localPosition.y, auxTransf.z);
                } else if (Axial) {
                    Vector3 auxTransf = new Vector3(ImplantInitialPos.x + auxPos.x/WidImpDif, ImplantInitialPos.y + auxPos.y/WidImpDif, Implant.transform.localPosition.z);
                    Implant.transform.localPosition = new Vector3(auxTransf.x, auxTransf.y, Implant.transform.localPosition.z);
                } else if (Sagittal) {
                    Vector3 auxTransf = new Vector3(Implant.transform.localPosition.x, ImplantInitialPos.y + auxPos.x/WidImpDif, ImplantInitialPos.z - auxPos.y/WidImpDif);
                    Implant.transform.localPosition = new Vector3(Implant.transform.localPosition.x, auxTransf.y, auxTransf.z);
                }
            } else if (Rotating) {
                Vector3 pos = Camera.ScreenToWorldPoint(MousePosition);
                Vector3 auxPos = pos - InitialPos;
                if (Coronal) {
                    float auxRot;
                    if (ImplantInitialRotation.y < -90)
                        auxRot = ImplantInitialRotation.y +360 - auxPos.x;
                    else if (ImplantInitialRotation.y > 90)
                        auxRot = ImplantInitialRotation.y -360 - auxPos.x;
                    else
                        auxRot = ImplantInitialRotation.y - auxPos.x;

                    if(auxRot > -90 && auxRot < 90)
                        Implant.transform.localEulerAngles = new Vector3(Implant.transform.localEulerAngles.x, auxRot, Implant.transform.localEulerAngles.z);
                } else if (Sagittal) {
                    float auxRot;
                    if (ImplantInitialRotation.x < -90)
                        auxRot = ImplantInitialRotation.x +360 + auxPos.x;
                    else if (ImplantInitialRotation.x > 90)
                        auxRot = ImplantInitialRotation.x -360 + auxPos.x;
                    else
                        auxRot = ImplantInitialRotation.x + auxPos.x;
                    if (auxRot > -90 && auxRot < 90)
                        Implant.transform.localEulerAngles = new Vector3(auxRot, Implant.transform.localEulerAngles.y, Implant.transform.localEulerAngles.z);
                }
            }

			if (Input.GetMouseButtonUp(0)) {
                Dragit = false;
				Getdir = true;
			}
            if (Input.GetMouseButtonUp(1))
                Rotating = false;
        }
    }

	private int MouseDirection(Vector3 auxPos) {
		Getdir = false;
		if (Mathf.Abs (auxPos.x) > Mathf.Abs (auxPos.y)) {
			return 0;//auxPos.y = 0;
		} else if (Mathf.Abs (auxPos.x) < Mathf.Abs (auxPos.y)) {
			return 1;//auxPos.x = 0; 
		} else
			return 2;
	}

    public void SliderSelected() {
        IsSliding = true;
    }

    public void SliderDeselected() {
        IsSliding = false;
    }

}
