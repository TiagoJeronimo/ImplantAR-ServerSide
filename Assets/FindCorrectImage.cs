using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindCorrectImage : MonoBehaviour {
	private GameObject axial;
	private GameObject sagittal;
	private GameObject coronal;
	private GameObject child;

	private float nrFilhosAxial;
	private float nrFilhosSagittal;
	private float nrFilhosCoronal;

	private float xDimension = 122 + 122;
	private float yDimension = 165 + 221;
	private float zDimension = 79 + 114;

	private float xMin=-122;
	private float yMin=-165;
	private float zMin=-79;

	private float x=0;
	private float y=0;
	private float z=0;

	private string fileName;
	private float nrImagem;

	public TextMesh axialText;
	public TextMesh sagittalText;
	public TextMesh coronalText;


	// Use this for initialization
	void Start () {
		axial = GameObject.Find ("Axial");
		sagittal = GameObject.Find ("Sagittal");
		coronal = GameObject.Find ("Coronal");
		nrFilhosAxial = axial.transform.childCount;
		nrFilhosSagittal = sagittal.transform.childCount;
		nrFilhosCoronal = coronal.transform.childCount;
		axialText.text = "Axial: ";
		sagittalText.text = "Sagittal: ";
		coronalText.text = "Coronal: ";
	}
	
	// Update is called once per frame
	void Update () {
		//Mudar imagem Sagittal
		if (transform.position.x != x) {
			x = transform.position.x;
			nrImagem = (x + xMin) / (-xDimension / nrFilhosSagittal);
			nrImagem = Mathf.Round (nrImagem);
			fileName = "IMG-0003-00" + (100 + nrImagem);
			sagittal.SetActiveRecursively (false);
			sagittal.SetActive (true);
			sagittal.transform.Find (fileName).gameObject.SetActive(true);
			sagittalText.text = "Sagittal: " + nrImagem + "/" + nrFilhosSagittal;
		}
		//Mudar imagem Axial
		if (transform.position.y != y) {
			y = transform.position.y;
			nrImagem = (y + yMin) / (-yDimension / nrFilhosAxial);
			nrImagem = Mathf.Round (nrImagem);

			if (nrImagem < 26) {
				fileName = "IMG-0004-000" + (74 + nrImagem);
			} else {
				fileName = "IMG-0004-00" + (74 + nrImagem);
			}
			axial.SetActiveRecursively (false);
			axial.SetActive (true);
			axial.transform.Find (fileName).gameObject.SetActive(true);
			axialText.text = "Axial: " + nrImagem + "/" + nrFilhosAxial;

		}
		//Mudar imagem Coronal
		if (transform.position.z != z) {
			z = transform.position.z;
			nrImagem = (z + zMin) / (-zDimension / nrFilhosCoronal);
			nrImagem = Mathf.Round (nrImagem);
			fileName = "IMG-0002-00" + (119 + nrImagem);

			coronal.SetActiveRecursively (false);
			coronal.SetActive (true);
			coronal.transform.Find (fileName).gameObject.SetActive(true);
			coronalText.text = "Coronal: " + nrImagem + "/" + nrFilhosCoronal;

		}
	}


}
