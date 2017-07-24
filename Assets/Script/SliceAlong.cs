using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceAlong : MonoBehaviour {

    public Slider mainSlider;
    public Text NumberOfSlices;

    public int SliceNumber;
    private int DiplayedFileNumber;
    private int LastMainSliderNumber;

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting() {
        //Displays the value of the slider in the console.
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        //Debug.Log("number in slide: " + DiplayedFileNumber);
        this.transform.GetChild(DiplayedFileNumber).gameObject.SetActive(true);

        //Debug.Log((int)mainSlider.value); //it's jumping some images
    }

    public void Update() {

        LastMainSliderNumber = (int)mainSlider.value;
        
        //SliceNumber = (int)mainSlider.value;
        if(DiplayedFileNumber != LastMainSliderNumber) {
            DiplayedFileNumber = LastMainSliderNumber;
            SliceNumber = DiplayedFileNumber;
        } else if(DiplayedFileNumber != SliceNumber) {
            DiplayedFileNumber = SliceNumber;
            //LastMainSliderNumber = SliceNumber;
            mainSlider.value = SliceNumber;
        }

        //mainSlider.value = SliceNumber;

        if (Input.GetAxis("Mouse ScrollWheel") != 0f) {      //not done yet
            SliceNumber += (int) Input.GetAxis("Mouse ScrollWheel");
            Debug.Log("Scrolling");
        }

        NumberOfSlices.text = "S: " + SliceNumber;
    }
}
