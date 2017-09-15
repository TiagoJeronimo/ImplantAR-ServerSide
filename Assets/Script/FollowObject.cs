using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform Target;
    public float Zoffset = -200.0f;
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3 (Target.transform.position.x, Target.transform.position.y, Zoffset);
    }
}
