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

    private SagittalCursorManager sagittalCursorManager;
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
        sagittalCursorManager = SagittalCursor.GetComponent<SagittalCursorManager>();
        axialCursorManager = AxialCursor.GetComponent<AxialCursorManager>();
    }

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
            sagittalCursorManager.UpdateSlide(sagittalChild);

            foreach (Transform child in AxialImages.transform) {
                 child.gameObject.SetActive(false);
             }
            //Negative 'cause 0 begins above
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
        mainSlider.value = sliceNumber;
        NumberOfSlices.text = "S: " + sliceNumber;
    }

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting() {
        foreach (Transform child in CoronalImages.transform) {
            child.gameObject.SetActive(false);
        }
        CoronalImages.transform.GetChild(DiplayedFileNumber).gameObject.SetActive(true);
        MapImageToCoordinate(DiplayedFileNumber, SagittalImages, SagittalCursor, ImageDimensions.x, sagittalCursorManager.ImageDimensionsPadding.x, 0);
        MapImageToCoordinate(DiplayedFileNumber, AxialImages, AxialCursor, ImageDimensions.y, axialCursorManager.ImageDimensionsPadding.y, 1); // slice image -> coordinates.  SliceNumber, Sl
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
