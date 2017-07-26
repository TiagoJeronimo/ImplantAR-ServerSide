using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SagittalCursorManager : MonoBehaviour {

    public GameObject AxialCursor;
    public GameObject CoronalCursor;

    public GameObject AxialImages;
    public GameObject CoronalImages;
    public GameObject SagittalImages;

    private CoronalCursorManager coronalCursorManager;
    private AxialCursorManager axialCursorManager;

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
    private int DiplayedFileNumber = 1;
    private int LastMainSliderNumber;

    //Limit position stuff
    private Vector3 LastPostion;

    void Start() {
        coronalCursorManager = CoronalCursor.GetComponent<CoronalCursorManager>();
        axialCursorManager = AxialCursor.GetComponent<AxialCursorManager>();
    }

    // Update is called once per frame
    void Update() {
        if (this.transform.position != LastPostion)
            CursorOutOffBoundsNoMouse(this.transform.position);

        if (Dragit ) {
            CoronalCursor.transform.localPosition = new Vector3(CoronalCursor.transform.localPosition.x, this.transform.localPosition.y /**/, CoronalCursor.transform.localPosition.z);
            AxialCursor.transform.localPosition = new Vector3(AxialCursor.transform.localPosition.x, -this.transform.localPosition.x, AxialCursor.transform.localPosition.z);

            foreach (Transform child in CoronalImages.transform) {
                child.gameObject.SetActive(false);
            }
            int coronalChild = MapCoordinatesToImage(this.transform.localPosition.x, CoronalImages, ImageDimensions.x, ImageDimensionsPadding.x);
            CoronalImages.transform.GetChild(coronalChild).gameObject.SetActive(true);
            coronalCursorManager.UpdateSlide(coronalChild);

            foreach (Transform child in AxialImages.transform) {
                child.gameObject.SetActive(false);
            }

            int axialChild = MapCoordinatesToImage(-this.transform.localPosition.y, AxialImages, ImageDimensions.y, ImageDimensionsPadding.y);
            AxialImages.transform.GetChild(axialChild).gameObject.SetActive(true);
            axialCursorManager.UpdateSlide(axialChild);
        }
        LastPostion = transform.position;

        DiplayedFileNumber = (int)mainSlider.value;
        NumberOfSlices.text = "S: " + DiplayedFileNumber;
    }

    private int MapCoordinatesToImage(float coord, GameObject imageType, float imageDimension, float imageDimensionPadding) { //makes a correlation between coordinates and the corresponding image
        float relation = imageType.transform.childCount / imageDimension;
        int imageNumber = Mathf.RoundToInt((coord - imageDimensionPadding) * relation);
        return imageNumber;
    }

    public void SlideToCoordinates(int slideValue) {
        float correspondentCoordinate = (slideValue * ImageDimensions.y) / SagittalImages.transform.childCount;
        CoronalCursor.transform.localPosition = new Vector3(correspondentCoordinate, CoronalCursor.transform.localPosition.y, CoronalCursor.transform.localPosition.z);
        AxialCursor.transform.localPosition = new Vector3(correspondentCoordinate, AxialCursor.transform.localPosition.y, AxialCursor.transform.localPosition.z);
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

    public void UpdateSlide(int sliceNumber) {
        if (sliceNumber > 0 && sliceNumber < SagittalImages.transform.childCount - 1) {
            //mainSlider.value = sliceNumber;
            //NumberOfSlices.text = "S: " + sliceNumber;
        }
    }

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting() {
        foreach (Transform child in SagittalImages.transform) {
            child.gameObject.SetActive(false);
        }
        SagittalImages.transform.GetChild(DiplayedFileNumber).gameObject.SetActive(true);
        MapImageToCoordinate(DiplayedFileNumber, CoronalImages, CoronalCursor, ImageDimensions.x, coronalCursorManager.ImageDimensionsPadding.x, 0);
        MapImageToCoordinate(DiplayedFileNumber, CoronalImages, AxialCursor, ImageDimensions.x, axialCursorManager.ImageDimensionsPadding.x, 0);
                                         //CoronalImages (255)? should be AxialImages (166)
    }

    //slice image -> coordinates. Rec: SliceNumber, what kind of slice we want to know the coordinates, cursor we want to know the coord, 
    //this image dimension, the padding of the slice we want to know the coordinates of, xOrY = we want to change the x or y
    private void MapImageToCoordinate(int imageNumber, GameObject imageType, GameObject cursor, float imageDimension, float imageDimensionPadding, int xOrY) {
        float relation = imageType.transform.childCount / imageDimension;
        int coord = Mathf.RoundToInt((imageNumber / relation) + imageDimensionPadding);
        if (xOrY == 0)
            cursor.transform.localPosition = new Vector3(coord, cursor.transform.localPosition.y, cursor.transform.localPosition.z);
        if (xOrY == 1)
            cursor.transform.localPosition = new Vector3(cursor.transform.localPosition.x, -coord, cursor.transform.localPosition.z);
    }
}
