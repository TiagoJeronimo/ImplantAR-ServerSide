using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

    public GameObject Cursor;
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = Cursor.transform.localPosition;
	}
}
