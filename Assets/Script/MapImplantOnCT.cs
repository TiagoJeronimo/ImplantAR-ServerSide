using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapImplantOnCT : MonoBehaviour {

    //Implant Widget
    public GameObject AxialImplantWidget;
    public GameObject CoronalImplantWidget;
    public GameObject SagittalImplantWidget;

    private float AxialInitialZ;
    private float CoronalInitialZ;
    private float SagittalInitialZ;

    void Start() {
        AxialInitialZ = AxialImplantWidget.transform.localPosition.z;
        CoronalInitialZ = CoronalImplantWidget.transform.localPosition.z;
        SagittalInitialZ = SagittalImplantWidget.transform.localPosition.z;
    }

	// Update is called once per frame
	void Update () {

		//Move Widgets
		AxialImplantWidget.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, AxialInitialZ + this.transform.localPosition.z);
		CoronalImplantWidget.transform.localPosition = new Vector3 (this.transform.localPosition.x, -this.transform.localPosition.z, CoronalInitialZ - this.transform.localPosition.y);
		SagittalImplantWidget.transform.localPosition = new Vector3 (-this.transform.localPosition.y, -this.transform.localPosition.z, SagittalInitialZ - this.transform.localPosition.x);

		//Rotate Widgets
		AxialImplantWidget.transform.localEulerAngles = this.transform.localEulerAngles;
		Quaternion auxCoronalRot = new Quaternion(-this.transform.localRotation.x, this.transform.localRotation.z, this.transform.localRotation.y, this.transform.localRotation.w) * Quaternion.Euler(90, 0, 0);
		CoronalImplantWidget.transform.localRotation = auxCoronalRot;
		Quaternion auxSagittalRot = new Quaternion(-this.transform.localRotation.y, this.transform.localRotation.z, this.transform.localRotation.x, this.transform.localRotation.w) * Quaternion.Euler(90, 0, 0);
		SagittalImplantWidget.transform.localRotation = auxSagittalRot; 

    }
}
