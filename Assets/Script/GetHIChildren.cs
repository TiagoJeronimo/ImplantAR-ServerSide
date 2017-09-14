using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Parabox.CSG;

public class GetHIChildren : MonoBehaviour {

    public GameObject Images;

    public GameObject CSGPrefab;
    public GameObject Implant;
    public GameObject Plan;

    private Vector3 LastPlanPosition;
    private Vector3 LastImplantPosition;

    GameObject composite;
    public Material myMaterial;

    // Use this for initialization
    void Start () {
        LastPlanPosition = Plan.transform.localPosition;
        LastImplantPosition = Implant.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(Plan.transform.localPosition != LastPlanPosition || Implant.transform.position != LastImplantPosition) {
            LastPlanPosition = Plan.transform.localPosition;
            LastImplantPosition = Implant.transform.localPosition;
            CSG_ops.CSG_calculations(Implant, Plan, CSGPrefab, 0);
        }
        /*foreach (Transform child in Images.transform) {
            if (child.gameObject.activeSelf) {
                if (child.CompareTag("HI")) {
                    //CSG_ops.CSG_calculations(OtherEx, ExamplePref, CSGPrefab, 0);
                    this.GetComponent<MeshRenderer>().enabled = true;
                    
                } else {
                    this.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }*/
    }
}
