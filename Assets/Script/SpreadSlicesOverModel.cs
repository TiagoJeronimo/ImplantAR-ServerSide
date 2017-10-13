using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSlicesOverModel : MonoBehaviour {

    public bool Sagittal; //Whith coord are we changing
    public bool Axial;
	public bool Coronal;
    private Vector3 AxialInitialPosition;
    private Vector3 SagittalInitialPosition;
    private Vector3 CoronalInitialPosition;

    public GameObject AxialImages;
	public GameObject CoronalImages;
	public GameObject SagittalImages;

    public GameObject Implant;


    private float Min;
    private float SlicePerUnity;
    private int LastSlice;

	// Use this for initialization
	void Start () {
        Vector3 ModelDimensions = Implant.GetComponent<BoxCollider>().bounds.size * 3;

        if (Sagittal) {
            SagittalInitialPosition = this.transform.localPosition;
            SlicePerUnity = ModelDimensions.x / SagittalImages.transform.childCount;
            Min = ModelDimensions.x / 2;
        } else if(Axial) {
            AxialInitialPosition = this.transform.localPosition;
			SlicePerUnity = ModelDimensions.z / AxialImages.transform.childCount;
            Min = ModelDimensions.z / 2;
        } else if(Coronal) {
            CoronalInitialPosition = this.transform.localPosition;
			SlicePerUnity = ModelDimensions.y / CoronalImages.transform.childCount;
            Min = ModelDimensions.y / 2;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, GetCoordPosition());
    }

    private float GetCoordPosition() {
        float coordPosition = 0.0f;

		GameObject Images;

		if (Sagittal) {
			Images = SagittalImages;
		} else if (Coronal) {
			Images = CoronalImages;
		} else {
			Images = AxialImages;
		}

        for(int i = 0; i < Images.transform.childCount; i++) {
            if (Images.transform.GetChild(i).gameObject.activeSelf) {
                if (LastSlice != i) {
                    LastSlice = i;
					if (Sagittal) {
						coordPosition = SagittalInitialPosition.z - Min + (i * SlicePerUnity);
					} else if (Axial) {
						coordPosition = AxialInitialPosition.z - Min + (i * SlicePerUnity);
					} else if (Coronal) {
						coordPosition = CoronalInitialPosition.z - Min + (i * SlicePerUnity);
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
