using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadSlicesOverModel : MonoBehaviour {

    public bool Sagittal;
    public bool Axial;
	public bool Coronal;
    public float CameraSize;
	public GameObject Images;
	//public GameObject Implant;
    public GameObject Model;

    public Vector3 ModelDimensions = new Vector3(82,60,75);

    private Vector3 InitialPosition;
    private float Min;
    private float SlicePerUnity;
    private int LastSlice;

    // Use this for initialization
    void Start () {
        //Vector3 ModelDimensions = Implant.GetComponent<BoxCollider>().size;
        //Implant.GetComponent<BoxCollider>().enabled = false;
        
        Bounds bounds = Model.GetComponent<MeshFilter>().mesh.bounds;
        Debug.Log("bounds: " + bounds.ToString());

        if (Sagittal) {
			InitialPosition = this.transform.localPosition;
			SlicePerUnity = CameraSize / Images.transform.childCount;
            Min = CameraSize/ 2;
        } else if(Axial) {
			InitialPosition = this.transform.localPosition;
			SlicePerUnity = CameraSize / Images.transform.childCount;
            Min = CameraSize/ 2;
        } else if(Coronal) {
			InitialPosition = this.transform.localPosition;
			SlicePerUnity = CameraSize / Images.transform.childCount;
            Min = CameraSize/ 2;
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
						coordPosition = -(InitialPosition.z + Min + (i * SlicePerUnity));
					} else if (Axial) {
						coordPosition = -(InitialPosition.z + Min + (i * SlicePerUnity));
					} else if (Coronal) {
						coordPosition = -(InitialPosition.z + Min + (i * SlicePerUnity));
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
