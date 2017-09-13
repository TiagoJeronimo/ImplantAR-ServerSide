using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSlicesOverModel : MonoBehaviour {

    public Vector3 ModelDimensions;
    public bool Sagittal; //Whith coord are we changing
    public bool Axial;
    public bool Coronal;

    public GameObject Images;

    private float Min;
    private float SlicePerUnity;
    private int LastSlice;

	// Use this for initialization
	void Start () {
        if (Sagittal) {
            SlicePerUnity = ModelDimensions.x / Images.transform.childCount;
            Min = ModelDimensions.x / 2;
        } else if(Axial) {
            SlicePerUnity = ModelDimensions.z / Images.transform.childCount;
            Min = -ModelDimensions.z / 2;
        } else if(Coronal) {
            SlicePerUnity = ModelDimensions.y/ Images.transform.childCount;
            Min = ModelDimensions.y / 2;
        }
    }
	
	// Update is called once per frame
	void Update () {
        MovePlane();
    }

    private float GetCoordPosition() {
        float coordPosition = 0.0f;
        int signalAux = 1;

        if (Coronal || Sagittal) {
            signalAux = -1;
        } else {
            signalAux = 1;
        }

        for(int i = 0; i < Images.transform.childCount; i++) {
            if (Images.transform.GetChild(i).gameObject.activeSelf) {
                if (LastSlice != i) {
                    LastSlice = i;
                    coordPosition = Min + (i * SlicePerUnity) * signalAux;
                } else if (Sagittal) {
                    coordPosition = this.transform.localPosition.x;
                } else if (Axial) {
                    coordPosition = this.transform.localPosition.z;
                } else if (Coronal) {
                    coordPosition = this.transform.localPosition.y;
                }
            } 
        }
        return coordPosition;
    }

    private void MovePlane() {
        if (Sagittal) this.transform.localPosition = new Vector3(GetCoordPosition(), this.transform.localPosition.y, this.transform.localPosition.z);
        if (Axial) this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, GetCoordPosition());
        if (Coronal) this.transform.localPosition = new Vector3(this.transform.localPosition.x, GetCoordPosition(), this.transform.localPosition.z);
    }
}
