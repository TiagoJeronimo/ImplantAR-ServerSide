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
            CoronalCursor.transform.localPosition = new Vector3(this.transform.localPosition.x, CoronalCursor.transform.localPosition.y /**/, CoronalCursor.transform.localPosition.z);
            SagitalCursor.transform.localPosition = new Vector3(-this.transform.localPosition.y, CoronalCursor.transform.localPosition.y, SagitalCursor.transform.localPosition.z);

            /*Limitar de 0 a 512, ou seja o tamanho da imagem*/

           foreach (Transform child in CoronalImages.transform) {
                child.gameObject.SetActive(false);
            }
            CoronalImages.transform.GetChild(MapCoordinatesToImage(-this.transform.localPosition.y, CoronalImages)).gameObject.SetActive(true);

            /*Limitar de 0 a 166, ou seja o tamanho da imagem*/

           foreach (Transform child in SagittalImages.transform) {
                child.gameObject.SetActive(false);
            }
            //Negative 'cause 0 begins above
            SagittalImages.transform.GetChild(MapCoordinatesToImage(this.transform.localPosition.x, SagittalImages)).gameObject.SetActive(true);
        }
    }

    private int MapCoordinatesToImage(float coord, GameObject imageType) { //makes a correlation between coordinates and the corresponding image
        float relation = imageType.transform.childCount / ImageDimensions.x;
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
        if (Dragit) {
            LastPostion = transform.position;
            Vector3 mousePos = pos - InitialPos; 
            transform.position = mousePos;
            if (transform.localPosition.x >= 0 && transform.localPosition.x <= ImageDimensions.x && transform.localPosition.y <= 0 && transform.localPosition.y > -ImageDimensions.y) { /**/ } 
            else
                transform.position = LastPostion;

        }
    }
    private void OnMouseUp() {
        Dragit = false;
    }
}
