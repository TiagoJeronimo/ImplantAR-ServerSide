using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPositionInEachSlice : MonoBehaviour {

    public GameObject AxialCursor;
    public GameObject SagittalCursor;
    public GameObject CoronalCursor;

    public GameObject AxialImages;
    public GameObject CoronalImages;
    public GameObject SagittalImages;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        CoronalCursor.transform.position = new Vector3(CoronalCursor.transform.position.x, SagittalCursor.transform.position.y /**/, CoronalCursor.transform.position.z);

        foreach (Transform child in CoronalImages.transform) {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild((int)SagittalCursor.transform.position.x).gameObject.SetActive(true);
    }
}
