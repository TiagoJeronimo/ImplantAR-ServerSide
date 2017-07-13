using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceAlong : MonoBehaviour {

    public Slider mainSlider;
    public Text NumberOfSlices;

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting() {
        //Displays the value of the slider in the console.
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild((int)mainSlider.value).gameObject.SetActive(true);

        //Debug.Log((int)mainSlider.value); //it's jumping some images
    }

    public void Update() {
        NumberOfSlices.text = "S: " + (int) mainSlider.value;
    }
}
