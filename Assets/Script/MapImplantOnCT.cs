using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapImplantOnCT : MonoBehaviour {

    public GameObject SagittalObject;
    public GameObject AxialObject;
    public GameObject CoronalObject;

    float NrImage;

    //valor mais baixo das coordenadas do modelo
    private float XMin = 60;
    private float YMin = -100;
    private float ZMin = -70;

    private float NrSagittalChildren;
    private float NrAxialChildren;
    private float NrCoronalChildren;

    //coordenadas somadas (caso centrado em 0)
    private float XDimension = 133 - 60;
    private float YDimension = 100 - 75;
    private float ZDimension = 70 - 15;

    private string FileName;

    private float x = 0;
    private float y = 0;
    private float z = 0;

    private bool Dragit;

    //Implant variables
    private float XImplantDimension = 3; //falta o raio do cilindro de protecção 
    private float YImplantDimension = 10;
    private float ZImplantDimension = 3; //falta o raio do cilindro de protecção 

    //Mapping Model to Cts stuff
    public GameObject AxialCursor;
    public GameObject CoronalCursor;
    public GameObject SagittalCursor;
    //public GameObject AxialImages;
    public Vector2 ImageDimensions;

    //public bool ShowImplantOnSlicesFromClient;

    //Implant Widget
    public GameObject CoronalImplantWidget;
    public GameObject SagittalImplantWidget;
    public GameObject AxialImplantWidget;

    public static bool UpdateCursorPositions;

    private Vector3 LastPostion;

    // Use this for initialization
    void Start () {
        NrSagittalChildren = SagittalObject.transform.childCount;
        NrAxialChildren = AxialObject.transform.childCount;
        NrCoronalChildren = CoronalObject.transform.childCount;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateCursorPositions = false;
        if ((transform.position.x != x || transform.position.y != y || transform.position.z != z)) {

            /*if(ShowImplantOnSlicesFromClient) {
                AxialCursor.transform.localPosition = new Vector3(this.transform.position.x, this.transform.localPosition.y + this.transform.parent.position.y, AxialCursor.transform.localPosition.z);
                if (ModelCoordToImageCoord(this.transform.position.y) < -85 && ModelCoordToImageCoord(this.transform.position.y) > -160) //prevent jitering, #find another solution
                    CoronalCursor.transform.localPosition = new Vector3(CoronalCursor.transform.localPosition.x, ModelCoordToImageCoord(this.transform.position.y), CoronalCursor.transform.localPosition.z);
                //SagittalCursor.transform.localPosition = new Vector3(SagittalCursor.transform.localPosition.x, ModelCoordToImageCoord(this.transform.position.y), SagittalCursor.transform.localPosition.z);

                foreach (Transform child in AxialImages.transform) {
                    child.gameObject.SetActive(false);
                }

                int imageNumber = MapCoordinatesToImage(this.transform.localPosition.z, AxialImages, YDimension); //YDimension is the height of the model
                if (imageNumber > 0 && imageNumber < AxialImages.transform.childCount)
                    AxialImages.transform.GetChild(imageNumber).gameObject.SetActive(true);
            }*/

			AxialImplantWidget.transform.localPosition = new Vector3 (128 - this.transform.localPosition.x, -128 + this.transform.localPosition.y, 50 + this.transform.localPosition.z);
			CoronalImplantWidget.transform.localPosition = new Vector3 (128 - this.transform.localPosition.x, -128 - this.transform.localPosition.z, 50 - this.transform.localPosition.y);
			SagittalImplantWidget.transform.localPosition = new Vector3 (128 - this.transform.localPosition.y, -128 - this.transform.localPosition.z, 50 - this.transform.localPosition.x);

            UpdateCursorPositions = true;
        }

		//Rotate Widgets like the Implant
		AxialImplantWidget.transform.localEulerAngles = this.transform.localEulerAngles;
		Quaternion auxCoronalRot = new Quaternion(-this.transform.localRotation.x, this.transform.localRotation.z, this.transform.localRotation.y, this.transform.localRotation.w) * Quaternion.Euler(90, 0, 0);
		CoronalImplantWidget.transform.localRotation = auxCoronalRot;
		Quaternion auxSagittalRot = new Quaternion(-this.transform.localRotation.y, this.transform.localRotation.z, this.transform.localRotation.x, this.transform.localRotation.w) * Quaternion.Euler(90, 0, 0);
		SagittalImplantWidget.transform.localRotation = auxSagittalRot; 

        //know which slices have the implant 
        if (transform.position.x != x) { //SAGITTAL

            foreach(Transform transformColor in SagittalObject.transform) {
                //transformColor.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                transformColor.tag = "Untagged";
            }

            x = transform.position.x; //posição do implante
            NrImage = ((x - XMin) * NrSagittalChildren) / XDimension;
            NrImage = Mathf.Round(NrImage); //center

            float ConvImplantX = (XImplantDimension * NrSagittalChildren) / XDimension;
            ConvImplantX = Mathf.Round(ConvImplantX);

            for (float i = NrImage - ( ConvImplantX / 2 ); i <= NrImage + (ConvImplantX / 2); i++) {
                i = Mathf.Round(i);
                if (NrImage < 10)
                    FileName = "IMG-0003-0000" + i;
                else if (NrImage < 100)
                    FileName = "IMG-0003-000" + i;
                else
                    FileName = "IMG-0003-00" + i;
                Transform file = SagittalObject.transform.Find(FileName);
                if (file) {
                    //file.GetComponent<SpriteRenderer>().color = Color.red; //if the sprite is red we know that this image has the implant in it
                    file.tag = "HI";
                }
            }
        }

        if (transform.position.y != y) { //AXIAL
            foreach (Transform transformColor in AxialObject.transform) {
                //transformColor.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                transformColor.tag = "Untagged";
            }

            y = transform.position.y; //posição do implante
            NrImage = ((y - YMin) * NrAxialChildren) / YDimension;
            NrImage = Mathf.Round(NrImage); //center

            float ConvImplantY = (YImplantDimension * NrAxialChildren) / YDimension;
            ConvImplantY = Mathf.Round(ConvImplantY);

            for (float i = NrImage - (ConvImplantY / 2); i <= NrImage + (ConvImplantY / 2); i++) {
                i = Mathf.Round(i);
                if (NrImage < 10)
                    FileName = "IM-0001-000" + i;
                else if (NrImage < 100)
                    FileName = "IM-0001-00" + i;
                else
                    FileName = "IM-0001-0" + i;
                Transform file = AxialObject.transform.Find(FileName);
                if (file) {
                    //file.GetComponent<SpriteRenderer>().color = Color.red; //if the sprite is red we know that this image has the implant in it
                    file.tag = "HI";
                }
            }
        }

        if (transform.position.z != z) { //CORONAL
            foreach (Transform transformColor in CoronalObject.transform) {
                //transformColor.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                transformColor.tag = "Untagged";
            }

            z = transform.position.z; //posição do implante
            NrImage = ((z - ZMin) * NrCoronalChildren) / ZDimension;
            NrImage = Mathf.Round(NrImage); //center

            float ConvImplantZ = (ZImplantDimension * NrCoronalChildren) / ZDimension;
            ConvImplantZ = Mathf.Round(ConvImplantZ);

            for (float i = NrImage - (ConvImplantZ / 2); i <= NrImage + (ConvImplantZ / 2); i++) {
                i = Mathf.Round(i);
                if (NrImage < 10)
                    FileName = "IMG-0002-0000" + i;
                else if (NrImage < 100)
                    FileName = "IMG-0002-000" + i;
                else
                    FileName = "IMG-0002-00" + i;
                Transform file = CoronalObject.transform.Find(FileName);
                if (file) {
                    //file.GetComponent<SpriteRenderer>().color = Color.red; //if the sprite is red we know that this image has the implant in it
                    file.tag = "HI";
                }
            }
        }
    }

    private float ModelCoordToImageCoord(float inModelCoord) {
        float y;
        y = (60 * inModelCoord) / YDimension + 86; 
        //60 = height of the coronal/sagittal image; 86 = height of the model's padding  
        //change this numbers
        return y;
    }

    private int MapCoordinatesToImage(float coord, GameObject imageType, float imageDimension) { //makes a correlation between coordinates and the corresponding image
        float relation = imageType.transform.childCount / imageDimension;
        int imageNumber = Mathf.RoundToInt(coord * relation) + 86; //86 = padding from the other type of slices
        return imageNumber;
    }

}
