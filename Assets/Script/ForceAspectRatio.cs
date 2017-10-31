using System.Collections;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour {

	private int LastWidth = Screen.width;
    private bool IsReseting = false;

    void Start()
	{
		Screen.SetResolution(750, 750, false);
	}

    void LateUpdate() {
        if (!IsReseting) {
            if (Screen.width != LastWidth) {
                // user is resizing width
                StartCoroutine(SetResolution());
                LastWidth = Screen.width;
            } else {
                // user is resizing height
                StartCoroutine(SetResolution());
            }
        }
    }

    IEnumerator SetResolution() {
        IsReseting = true;
        Screen.fullScreen = !Screen.fullScreen;
        Screen.SetResolution(Screen.width, Screen.width, false);
        yield return new WaitForSeconds(0.5F);
        IsReseting = false;
    }
}
