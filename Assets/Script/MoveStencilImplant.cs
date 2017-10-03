using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStencilImplant : MonoBehaviour {

	public Transform Implant;

	public bool Axial;
	public bool Sagittal;
	public bool Coronal;

	private Vector3 ImplantInitialPos;

	private Vector3 MousePosition;
	private Vector3 InitialPos;
	private bool Dragit = false;
	private float DistZ = 0;

	private void OnMouseDown() {
		if (Input.GetMouseButtonDown(0)) {
			MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
			InitialPos = Camera.main.ScreenToWorldPoint(MousePosition);
			ImplantInitialPos = Implant.transform.localPosition;
		}
		Dragit = true;
	}

	private void OnMouseDrag() {
		MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);

		if (Dragit) {
			Vector3 pos = Camera.main.ScreenToWorldPoint(MousePosition);
			Vector3 auxPos = pos - InitialPos;
			if (Coronal) {
				Vector3 axuTransf = new Vector3(ImplantInitialPos.x - auxPos.x, Implant.transform.localPosition.y, ImplantInitialPos.z - auxPos.y);
				Implant.transform.localPosition = new Vector3 (axuTransf.x, Implant.transform.localPosition.y, axuTransf.z);
			}
			else if (Axial) {
				Vector3 auxTransf = new Vector3(ImplantInitialPos.x - auxPos.x, ImplantInitialPos.y + auxPos.y, Implant.transform.localPosition.z);
				Implant.transform.localPosition = new Vector3(auxTransf.x, auxTransf.y, Implant.transform.localPosition.z);
			} 
			else if (Sagittal) {
				Vector3 auxTransf = new Vector3(Implant.transform.localPosition.x, ImplantInitialPos.y - auxPos.x, ImplantInitialPos.z - auxPos.y);
				Implant.transform.localPosition = new Vector3(Implant.transform.localPosition.x, auxTransf.y, auxTransf.z);
			}
		}
	}

	private void OnMouseUp() {
		Dragit = false;
	}

}
