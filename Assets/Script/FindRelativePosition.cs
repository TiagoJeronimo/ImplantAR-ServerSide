using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRelativePosition : MonoBehaviour {

    public static Vector3 PositionRelativeToJaw;
    public GameObject Implant;

    void Update () {
        PositionRelativeToJaw = this.transform.position - Implant.transform.position;
        //Debug.Log("PositonRealativeJAw: " + PositionRelativeToJaw.ToString());
    }
}
