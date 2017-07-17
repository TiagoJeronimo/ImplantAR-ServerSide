using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapImplantOnCT : MonoBehaviour {

    public GameObject SagittalObject;
    public GameObject AxialObject;
    public GameObject CoronalObject;

    float NrImage;

    //valor mais baixo das coordenadas do modelo
    private float XMin = 91;
    private float YMin = -102;
    private float ZMin = -70;

    private float NrSagittalChildren;
    private float NrAxialChildren;
    private float NrCoronalChildren;

    //coordenadas somadas (caso centrado em 0)
    private float XDimension = 155 - 91;
    private float YDimension = 102 - 74;
    private float ZDimension = 70 - 15;

    private string FileName;

    private float x = 0;
    private float y = 0;
    private float z = 0;

    //Implant variables
    private float XImplantDimension = 3; //falta o raio do cilindro de protecção 
    private float YImplantDimension = 10;
    private float ZImplantDimension = 3; //falta o raio do cilindro de protecção 

    // Use this for initialization
    void Start () {
        NrSagittalChildren = SagittalObject.transform.childCount;
        NrAxialChildren = AxialObject.transform.childCount;
        NrCoronalChildren = CoronalObject.transform.childCount;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x != x) {

            foreach(Transform transformColor in SagittalObject.transform) {
                transformColor.GetComponentInChildren<SpriteRenderer>().color = Color.white;
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
                //Debug.Log("Filename required: " + FileName);
                Transform file = SagittalObject.transform.Find(FileName);
                if (file)
                    file.GetComponent<SpriteRenderer>().color = Color.red; //if the sprite is red we know that this image has the implant in it
            }
        }

        if (transform.position.y != y) {
            foreach (Transform transformColor in AxialObject.transform) {
                transformColor.GetComponentInChildren<SpriteRenderer>().color = Color.white;
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
                //Debug.Log("Filename required: " + FileName);
                Transform file = AxialObject.transform.Find(FileName);
                if (file)
                    file.GetComponent<SpriteRenderer>().color = Color.red; //if the sprite is red we know that this image has the implant in it
            }
        }

        if (transform.position.z != z) {
            foreach (Transform transformColor in CoronalObject.transform) {
                transformColor.GetComponentInChildren<SpriteRenderer>().color = Color.white;
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
                //Debug.Log("Filename required: " + FileName);
                Transform file = CoronalObject.transform.Find(FileName);
                if (file)
                    file.GetComponent<SpriteRenderer>().color = Color.red; //if the sprite is red we know that this image has the implant in it
            }
        }
    }
}
