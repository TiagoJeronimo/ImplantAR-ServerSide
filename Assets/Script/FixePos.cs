using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixePos : MonoBehaviour {

    public Transform Implant;
    public Transform Plan;

    public bool Axial;
    public bool Sagittal;
    public bool Coronal;

    Vector3 LastPlanPosition;

    void Start() {
        LastPlanPosition = Plan.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //this.transform.localPosition = Implant.transform.position;
        if(Axial) this.transform.localPosition = new Vector3(-Implant.transform.position.x, this.transform.localPosition.y, -Implant.transform.position.z); //AXIAL
        if(Sagittal) this.transform.localPosition = new Vector3(this.transform.localPosition.x, -Implant.transform.position.y, -Implant.transform.position.z); //SAGITTAL
        if (Coronal) this.transform.localPosition = new Vector3(-Implant.transform.position.x, -Implant.transform.position.y, this.transform.localPosition.z); //CORONAL

       /* if(LastPlanPosition != Plan.transform.position) {
            LastPlanPosition = Plan.transform.position;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, -Plan.transform.localPosition.y, this.transform.localPosition.z);
        }*/
    }
}
