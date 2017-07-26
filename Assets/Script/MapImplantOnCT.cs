﻿using System.Collections;
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
    public GameObject AxialImages;
    public Vector2 ImageDimensions;

    public bool ShowImplantOnSlicesFromClient;

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

            if(ShowImplantOnSlicesFromClient) {
                AxialCursor.transform.localPosition = new Vector3(this.transform.position.x, this.transform.localPosition.y + this.transform.parent.position.y, AxialCursor.transform.localPosition.z);
                CoronalCursor.transform.localPosition = new Vector3(CoronalCursor.transform.localPosition.x, ModelCoordToImageCoord(this.transform.position.y), CoronalCursor.transform.localPosition.z);
                SagittalCursor.transform.localPosition = new Vector3(SagittalCursor.transform.localPosition.x, ModelCoordToImageCoord(this.transform.position.y), SagittalCursor.transform.localPosition.z);
                //                                                                                             ^ModelCoordToImageCoord don't work

                foreach (Transform child in AxialImages.transform) {
                    child.gameObject.SetActive(false);
                }

                int imageNumber = MapCoordinatesToImage(this.transform.localPosition.z + 90, AxialImages, ImageDimensions.y); //ver este 90
                if (imageNumber > 0 && imageNumber < AxialImages.transform.childCount)
                    AxialImages.transform.GetChild(imageNumber).gameObject.SetActive(true);
            }

            AxialImplantWidget.transform.localPosition = new Vector3(this.transform.position.x, this.transform.localPosition.y + this.transform.parent.position.y, AxialCursor.transform.localPosition.z);
            CoronalImplantWidget.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, CoronalCursor.transform.localPosition.z);
            SagittalImplantWidget.transform.localPosition = new Vector3(-this.transform.localPosition.y - this.transform.parent.position.y, this.transform.position.y, SagittalCursor.transform.localPosition.z);

            UpdateCursorPositions = true;
        }

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
                SagittalCursor.GetComponent<SagittalCursorManager>().UpdateSlide((int)i);
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
                AxialCursor.GetComponent<AxialCursorManager>().UpdateSlide((int)i);
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
                CoronalCursor.GetComponent<CoronalCursorManager>().UpdateSlide((int)i);
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
        //not working
        float y = (80 * inModelCoord) / YMin; //80=Min das imagens (mudar isto)
        return y;
    }

    private int MapCoordinatesToImage(float coord, GameObject imageType, float imageDimension) { //makes a correlation between coordinates and the corresponding image
        float relation = imageType.transform.childCount / imageDimension;
        int imageNumber = Mathf.RoundToInt(coord * relation) - 80; //padding from the other type of slices
        return imageNumber;
    }

}
