using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowImplant : MonoBehaviour {

    public Transform Implant;

	// Update is called once per frame
	void Update () {
        this.transform.localPosition = Implant.transform.localPosition;
	}
}
