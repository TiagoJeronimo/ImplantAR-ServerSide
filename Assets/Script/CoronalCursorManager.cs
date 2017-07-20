using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronalCursorManager : MonoBehaviour {

    public GameObject AxialCursor;
    public GameObject SagittalCursor;

    public GameObject AxialImages;
    public GameObject CoronalImages;
    public GameObject SagittalImages;

    public Vector2 ImageDimensions;

    //PAN STUFF//
    private Vector3 MousePosition;
    private Vector3 InitialPos;
    private bool Dragit;
    float DistZ = 0;

    //Limit position stuff
    private Vector3 LastPostion;

    // Update is called once per frame
    void Update() {
        if (Dragit) {
            SagittalCursor.transform.localPosition = new Vector3(SagittalCursor.transform.localPosition.x, this.transform.localPosition.y /**/, SagittalCursor.transform.localPosition.z);
            AxialCursor.transform.localPosition = new Vector3(this.transform.localPosition.x, AxialCursor.transform.localPosition.y, AxialCursor.transform.localPosition.z);

            /*Limitar de 0 a 512, ou seja o tamanho da imagem*/

            foreach (Transform child in SagittalImages.transform) {
                child.gameObject.SetActive(false);
            }
            SagittalImages.transform.GetChild(MapCoordinatesToImage(this.transform.localPosition.x, SagittalImages, ImageDimensions.x)).gameObject.SetActive(true);

            /*Limitar de 0 a 166, ou seja o tamanho da imagem*/

            foreach (Transform child in AxialImages.transform) {
                 child.gameObject.SetActive(false);
             }
             //Negative 'cause 0 begins above
             AxialImages.transform.GetChild(MapCoordinatesToImage(- this.transform.localPosition.y, AxialImages, ImageDimensions.y)).gameObject.SetActive(true);
        }
    }

    private int MapCoordinatesToImage(float coord, GameObject imageType, float imageDimension) { //makes a correlation between coordinates and the corresponding image
        float relation = imageType.transform.childCount / imageDimension;
        int imageNumber = Mathf.RoundToInt(coord * relation);
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
        if (transform.localPosition.x >= 0 && transform.localPosition.x <= ImageDimensions.x && transform.localPosition.y <= 0 && transform.localPosition.y > -ImageDimensions.y) { /**/ } else
            transform.position = LastPostion;
    }
}
