using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoronalCursorManager : MonoBehaviour {

    public GameObject AxialCursor;
    public GameObject SagittalCursor;

    public GameObject AxialImages;
    public GameObject CoronalImages;
    public GameObject SagittalImages;

    public Vector2 ImageDimensions;
    public Vector2 ImageDimensionsPadding;

    //PAN STUFF//
    private Vector3 MousePosition;
    private Vector3 InitialPos;
    private bool Dragit;
    float DistZ = 0;

    //SLIDER STUFF//
    public Slider mainSlider;
    public Text NumberOfSlices;
    public int SliceNumber;
    private int DiplayedFileNumber = 1;
    private int LastMainSliderNumber;

    //Limit position stuff
    private Vector3 LastPostion;

    // Update is called once per frame
    void Update() {
        if (this.transform.position != LastPostion)
            CursorOutOffBoundsNoMouse(this.transform.position);

        if (Dragit) {
            SagittalCursor.transform.localPosition = new Vector3(SagittalCursor.transform.localPosition.x, this.transform.localPosition.y /**/, SagittalCursor.transform.localPosition.z);
            AxialCursor.transform.localPosition = new Vector3(this.transform.localPosition.x, AxialCursor.transform.localPosition.y, AxialCursor.transform.localPosition.z);

            foreach (Transform child in SagittalImages.transform) {
                child.gameObject.SetActive(false);
            }
            int sagittalChild = MapCoordinatesToImage(this.transform.localPosition.x, SagittalImages, ImageDimensions.x, ImageDimensionsPadding.x);
            SagittalImages.transform.GetChild(sagittalChild).gameObject.SetActive(true);
            SagittalCursor.GetComponent<SagittalCursorManager>().SliceNumber = sagittalChild;

            foreach (Transform child in AxialImages.transform) {
                 child.gameObject.SetActive(false);
             }
            //Negative 'cause 0 begins above
            int axialChild = MapCoordinatesToImage(-this.transform.localPosition.y, AxialImages, ImageDimensions.y, ImageDimensionsPadding.y);
            AxialImages.transform.GetChild(axialChild).gameObject.SetActive(true);
            AxialCursor.GetComponent<AxialCursorManager>().SliceNumber = axialChild;
        }
        LastPostion = transform.position;

        UpdateSlide();
    }

    private int MapCoordinatesToImage(float coord, GameObject imageType, float imageDimension, float imageDimensionPadding) { //makes a correlation between coordinates and the corresponding image
        float relation = imageType.transform.childCount / imageDimension;
        int imageNumber = Mathf.RoundToInt((coord - imageDimensionPadding) * relation);
        return imageNumber;
    }

    public void SlideToCoordinates(int slideValue) {
        float axialCorrespondentCoordinate = (slideValue * ImageDimensions.y) / CoronalImages.transform.childCount;
        float sagittalCorrespondentCoordinate = (slideValue * ImageDimensions.x) / CoronalImages.transform.childCount;
        AxialCursor.transform.localPosition = new Vector3(AxialCursor.transform.localPosition.x, axialCorrespondentCoordinate, AxialCursor.transform.localPosition.z);
        SagittalCursor.transform.localPosition = new Vector3(sagittalCorrespondentCoordinate, SagittalCursor.transform.localPosition.y, SagittalCursor.transform.localPosition.z);
    }

    //PAN STUFF///

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
            InitialPos = Camera.main.ScreenToWorldPoint(MousePosition) - transform.position;
        }
        Dragit = true;
    }
    private void OnMouseDrag() {
        MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistZ);
        Vector3 pos = Camera.main.ScreenToWorldPoint(MousePosition);
        if (Dragit)
            CursorOutOfBounds(pos);
    }
    private void OnMouseUp() {
        Dragit = false;
    }

    private void CursorOutOfBounds(Vector3 pos) {
        LastPostion = transform.position;
        Vector3 mousePos = pos - InitialPos;
        transform.position = mousePos;
        if (transform.localPosition.x >= ImageDimensionsPadding.x && transform.localPosition.x <= ImageDimensions.x + ImageDimensionsPadding.x && transform.localPosition.y <= -ImageDimensionsPadding.y && transform.localPosition.y > -ImageDimensions.y - ImageDimensionsPadding.y) { /**/ } else
            transform.position = LastPostion;
    }

    private void CursorOutOffBoundsNoMouse(Vector3 pos) {
        transform.position = pos;
        if (transform.localPosition.x >= ImageDimensionsPadding.x && transform.localPosition.x <= ImageDimensions.x + ImageDimensionsPadding.x && transform.localPosition.y <= -ImageDimensionsPadding.y && transform.localPosition.y > -ImageDimensions.y - ImageDimensionsPadding.y) { /**/ } else
            transform.position = LastPostion;
    }

    //SLIDER STUFF//

    private void UpdateSlide() {
        LastMainSliderNumber = (int)mainSlider.value;
        
        //SliceNumber = (int)mainSlider.value;
        if (DiplayedFileNumber != LastMainSliderNumber) {
            DiplayedFileNumber = LastMainSliderNumber;
            SliceNumber = DiplayedFileNumber;
        } else if (DiplayedFileNumber != SliceNumber) {
            DiplayedFileNumber = SliceNumber;
            //LastMainSliderNumber = SliceNumber;
            mainSlider.value = SliceNumber;
        }
        //mainSlider.value = SliceNumber;
        NumberOfSlices.text = "S: " + SliceNumber;
    }

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting() {
        //Displays the value of the slider in the console.
        foreach (Transform child in CoronalImages.transform) {
            child.gameObject.SetActive(false);
        }
        //Debug.Log("number in slide Coronal: " + DiplayedFileNumber);
        CoronalImages.transform.GetChild(DiplayedFileNumber).gameObject.SetActive(true);
        SlideToCoordinates(LastMainSliderNumber);
    }

}
