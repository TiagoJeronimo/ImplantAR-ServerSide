using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSlicesOverModel : MonoBehaviour {

    public Vector3 ModelDimensions;
    public bool Sagittal; //Whith coord are we changing
    public bool Axial;
	public bool Coronal;
    private Vector3 AxialInitialPosition;
	private Vector3 CoronalInitialPosition;
	private Vector3 SagittalInitialPosition;

	public Transform SagittalWidgetTransform;
	public Transform CoronalWidgetTransform;

    public GameObject AxialImages;
	public GameObject CoronalImages;
	public GameObject SagittalImages;


    private float Min;
    private float SlicePerUnity;
    private int LastSlice;

	// Use this for initialization
	void Start () {
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
	void Update () {
        MovePlane();
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
						coordPosition = SagittalWidgetTransform.localPosition.z - Min + (i * SlicePerUnity);
						//don't work with rotations
						/*if (coordPosition >= SagittalWidgetTransform.localPosition.z)
							coordPosition = SagittalWidgetTransform.localPosition.z + Min - (i * SlicePerUnity);*/
					} else if (Axial) {
						coordPosition = AxialInitialPosition.z - Min + (i * SlicePerUnity);
					} else if (Coronal) {
						coordPosition = CoronalWidgetTransform.localPosition.z - Min + (i * SlicePerUnity);
						//don't work with rotations
						/*if (coordPosition >= CoronalWidgetTransform.localPosition.z)
							coordPosition = CoronalWidgetTransform.localPosition.z + Min - (i * SlicePerUnity);*/
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

    private void MovePlane() {
		this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, GetCoordPosition());
    }
}
