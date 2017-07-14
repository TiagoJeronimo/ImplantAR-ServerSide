using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapImplantOnCT : MonoBehaviour {

    public GameObject SagitalObject;

    float NrImage;

    //valor mais baixo das coordenadas do modelo
    private float XMin = 0; //inserir

    private float NrSagittalChildren;

    //coordenadas somadas (caso centrado em 0)
    private float XDimension = 0 + 0; //inserir

    private string FileName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float x = transform.position.x; //posição do implante
        NrImage = (x + XMin) / (-XDimension / NrSagittalChildren);
        NrImage = Mathf.Round(NrImage);
        FileName = "IMG-0003-000" + NrImage;
        SagitalObject.transform.Find(FileName).GetComponent<SpriteRenderer>().color = Color.red; //if the sprite is red we know that this image has the implant in it (por agora sabemos o centro do implante)
    }
}
