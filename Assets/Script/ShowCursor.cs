using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour {

    public GameObject AxialCursor;
    public GameObject SagittalCursor;
    public GameObject CoronalCursor;

    public void ShowHideCursor() {
        if (AxialCursor.GetComponent<CircleCollider2D>().enabled) {
            AxialCursor.GetComponent<CircleCollider2D>().enabled = false;
            AxialCursor.GetComponent<SpriteRenderer>().enabled = false;
            CoronalCursor.GetComponent<CircleCollider2D>().enabled = false;
            CoronalCursor.GetComponent<SpriteRenderer>().enabled = false;
            SagittalCursor.GetComponent<CircleCollider2D>().enabled = false;
            SagittalCursor.GetComponent<SpriteRenderer>().enabled = false;
        } else {
            AxialCursor.GetComponent<CircleCollider2D>().enabled = true;
            AxialCursor.GetComponent<SpriteRenderer>().enabled = true;
            CoronalCursor.GetComponent<CircleCollider2D>().enabled = true;
            CoronalCursor.GetComponent<SpriteRenderer>().enabled = true;
            SagittalCursor.GetComponent<CircleCollider2D>().enabled = true;
            SagittalCursor.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
