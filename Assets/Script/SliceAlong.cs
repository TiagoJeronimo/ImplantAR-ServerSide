using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceAlong : MonoBehaviour {

    public Slider mainSlider;
    public Text NumberOfSlices;

    private int SliceNumber;

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting() {
        //Displays the value of the slider in the console.
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild(SliceNumber).gameObject.SetActive(true);

        //Debug.Log((int)mainSlider.value); //it's jumping some images
    }

    public void Update() {
        SliceNumber = (int)mainSlider.value;
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) {      
            SliceNumber += (int) Input.GetAxis("Mouse ScrollWheel");
            Debug.Log("Scrolling");
        }

        NumberOfSlices.text = "S: " + SliceNumber;
    }
}
