using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour {

    public Camera Camera;

    //SLIDER STUFF//
    public Slider mainSlider;
    public int DiplayedFileNumber = 1;

    //MOUSE SCROLL stuff
    private int ScrollWheelValue = 0;
    private Ray Ray;
    private RaycastHit2D Hit;

    // Use this for initialization
    void Start () {
        foreach (Transform child in this.transform) {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild(DiplayedFileNumber).gameObject.SetActive(true);
        mainSlider.value = DiplayedFileNumber;
    }
	
	// Update is called once per frame
	void Update () {
        Ray = Camera.ScreenPointToRay(Input.mousePosition);
        Hit = Physics2D.Raycast(Ray.origin, Ray.direction);
        if (Hit) {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f) {
                ScrollWheelValue += Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 100);
                ScrollWheelValue = Mathf.Clamp(ScrollWheelValue, 0, 600);//prevents value from exceeding specified range
                mainSlider.value = ScrollWheelValue;
            } else {
                ScrollWheelValue = (int)mainSlider.value;
            }
        }

        DiplayedFileNumber = (int)mainSlider.value;
    }

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting() {
        foreach (Transform child in this.transform) {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild(DiplayedFileNumber).gameObject.SetActive(true);
    }
}
