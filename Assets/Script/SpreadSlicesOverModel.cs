using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSlicesOverModel : MonoBehaviour {

    public Vector3 ModelDimensions;
    public Vector3 ModelPosition;
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
            Min = ModelPosition.x - ModelDimensions.x / 2;
        } else if(Axial) {
            SlicePerUnity = ModelDimensions.y / Images.transform.childCount;
            Min = ModelPosition.y - ModelDimensions.y / 2;
        } else if(Coronal) {
            SlicePerUnity = ModelDimensions.z / Images.transform.childCount;
            Min = ModelPosition.z - ModelDimensions.z / 2;
        }
    }
	
	// Update is called once per frame
	void Update () {
        MovePlane();
    }

    private float GetCoordPosition() {
        float coordPosition = 0; 
        for(int i = 0; i < Images.transform.childCount; i++) {
            if (Images.transform.GetChild(i).gameObject.activeSelf) {
                if (LastSlice != i) {
                    LastSlice = i;
                    coordPosition = Min + (i * SlicePerUnity);
                } else if (Sagittal) {
                    coordPosition = this.transform.position.x;
                    Debug.Log("sagital coorX: " + coordPosition);
                } else if (Axial) {
                    coordPosition = this.transform.position.y;
                } else if (Coronal) {
                    coordPosition = this.transform.position.z;
                }
            } 
        }
        return coordPosition;
    }

    private void MovePlane() {
        if (Sagittal) this.transform.position = new Vector3(GetCoordPosition(), ModelPosition.y, ModelPosition.z);
        if (Axial) this.transform.position = new Vector3(ModelPosition.x, GetCoordPosition(), ModelPosition.z);
        if (Coronal) this.transform.position = new Vector3(ModelPosition.x, ModelPosition.y, GetCoordPosition());
    }
}
