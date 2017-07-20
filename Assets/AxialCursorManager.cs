using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxialCursorManager : MonoBehaviour {

    public GameObject SagitalCursor;
    public GameObject CoronalCursor;

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

    //Limit position stuff
    private Vector3 LastPostion;

    // Update is called once per frame
    void Update() {
        if(this.transform.position != LastPostion)
            CursorOutOffBoundsNoMouse(this.transform.position);

        if (Dragit || MapImplantOnCT.UpdateCursorPositions) {
            CoronalCursor.transform.localPosition = new Vector3(this.transform.localPosition.x, CoronalCursor.transform.localPosition.y /**/, CoronalCursor.transform.localPosition.z);
            SagitalCursor.transform.localPosition = new Vector3(-this.transform.localPosition.y, CoronalCursor.transform.localPosition.y, SagitalCursor.transform.localPosition.z);

           foreach (Transform child in CoronalImages.transform) {
                child.gameObject.SetActive(false);
            }
            CoronalImages.transform.GetChild(MapCoordinatesToImage(-this.transform.localPosition.y, CoronalImages, ImageDimensions.y, ImageDimensionsPadding.y)).gameObject.SetActive(true);

           foreach (Transform child in SagittalImages.transform) {
                child.gameObject.SetActive(false);
            }
            //Negative 'cause 0 begins above
            SagittalImages.transform.GetChild(MapCoordinatesToImage(this.transform.localPosition.x, SagittalImages, ImageDimensions.x, ImageDimensionsPadding.x)).gameObject.SetActive(true);
        }
        LastPostion = transform.position;
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
        if (Dragit) {
            CursorOutOffBounds(pos);
        }
    }
    private void OnMouseUp() {
        Dragit = false;
    }

    private void CursorOutOffBounds(Vector3 pos) {
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
}
