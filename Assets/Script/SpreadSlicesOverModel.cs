using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSlicesOverModel : MonoBehaviour {

    public bool Sagittal;
    public bool Axial;
	public bool Coronal;
	public GameObject Images;
	//public GameObject Implant;

    public Vector3 ModelDimensions = new Vector3(80,80,50);

    private Vector3 InitialPosition;
    private float Min;
    private float SlicePerUnity;
    private int LastSlice;

    // Use this for initialization
    void Start () {
        //Vector3 ModelDimensions = Implant.GetComponent<BoxCollider>().size;
        //Implant.GetComponent<BoxCollider>().enabled = false;

        if (Sagittal) {
			InitialPosition = this.transform.localPosition;
			SlicePerUnity = ModelDimensions.x / Images.transform.childCount;
            Min = ModelDimensions.x / 2;
            Debug.Log("Sagittal ModelDimensions.x : " + ModelDimensions.x);
        } else if(Axial) {
			InitialPosition = this.transform.localPosition;
			SlicePerUnity = ModelDimensions.z / Images.transform.childCount;
            Min = ModelDimensions.z / 2;
            Debug.Log("Axial ModelDimensions.z: " + ModelDimensions.z);
        } else if(Coronal) {
			InitialPosition = this.transform.localPosition;
			SlicePerUnity = ModelDimensions.y / Images.transform.childCount;
            Min = ModelDimensions.y / 2;
            Debug.Log("Coronal ModelDimensions.y: " + ModelDimensions.y);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, GetCoordPosition());
    }

    private float GetCoordPosition() {
        float coordPosition = 0.0f;

        for(int i = 1; i < Images.transform.childCount; i++) {
            if (Images.transform.GetChild(i).gameObject.activeSelf) {
                if (LastSlice != i) {
                    LastSlice = i;
					if (Sagittal) {
						coordPosition = InitialPosition.z - Min + (i * SlicePerUnity);
					} else if (Axial) {
						coordPosition = InitialPosition.z - Min + (i * SlicePerUnity);
					} else if (Coronal) {
						coordPosition = InitialPosition.z - Min + (i * SlicePerUnity);
					}
                } else if (Sagittal) {
                    coordPosition = this.transform.localPosition.z;
                } else if (Axial) {
                    coordPosition = this.transform.localPosition.z;
                } else if (Coronal) {
                    coordPosition = this.transform.localPosition.z;
                }
            } 
        }
        return coordPosition;
    }
}
