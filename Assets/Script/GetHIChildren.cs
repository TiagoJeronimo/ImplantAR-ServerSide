using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHIChildren : MonoBehaviour {

    public GameObject Images;

    public GameObject CSGPrefab;
    public GameObject OtherEx;
    public GameObject ExamplePref;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in Images.transform) {
            if (child.gameObject.activeSelf) {
                if (child.CompareTag("HI")) {
                    CSG_ops.CSG_calculations(OtherEx, ExamplePref, CSGPrefab, 0);
                    this.GetComponent<MeshRenderer>().enabled = true;
                    
                } else {
                    this.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }
}
