using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Parabox.CSG;

public class GetHIChildren : MonoBehaviour {

    public GameObject CSGPrefab;

    private Vector3 LastPlanPosition;
    private Vector3 LastImplantPosition;

    void Start() {
        LastPlanPosition = this.transform.position;
        LastImplantPosition = this.transform.position;
    }

    void OnTriggerStay(Collider other) {
        if (LastPlanPosition != this.transform.position || LastImplantPosition != other.transform.position) {
            LastPlanPosition = this.transform.position;
            LastImplantPosition = other.transform.position;
            CSG_ops.CSG_calculations(other.gameObject, this.gameObject, CSGPrefab, 0);
        }
    }
}
