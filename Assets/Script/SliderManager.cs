using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour {

    public Camera Camera;

    //SLIDER
    public Slider slider;

    //MOUSE SCROLL
    private Ray Ray;
    private RaycastHit2D Hit;

    // Use this for initialization
    void Awake () {
        slider.onValueChanged.AddListener(HandleSliderChanged);
    }


    void Start () {
        HandleSliderChanged(slider.value);
    }
	
	// Update is called once per frame
	void Update () {
        Ray = Camera.ScreenPointToRay(Input.mousePosition);
        Hit = Physics2D.Raycast(Ray.origin, Ray.direction);
        if (Hit) {
            if (Input.GetAxis("Mouse ScrollWheel") != 0f) {
                slider.value += Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 100);
                slider.value = Mathf.Clamp(slider.value, 0, 512);//prevents value from exceeding specified range
                HandleSliderChanged(slider.value);
            }
        }
    }

    private void HandleSliderChanged(float value)
    {
        foreach (Transform child in this.transform) {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild((int)value-1).gameObject.SetActive(true);
    }
}
