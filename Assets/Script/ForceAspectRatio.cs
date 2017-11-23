using System.Collections;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour {

	private int LastWidth = Screen.width;
    private bool IsReseting = false;

    void Start()
	{
		Screen.SetResolution(Screen.height, Screen.height, true);
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.F11)) {
			Screen.fullScreen = !Screen.fullScreen;
		}

		if (Input.GetKey("escape"))
			Application.Quit();
	}

    void LateUpdate() {
		if (!Screen.fullScreen) {
			if (!IsReseting) {
				if (Screen.width != LastWidth) {
					// user is resizing width
					StartCoroutine (SetResolution ());
					LastWidth = Screen.width;
				} else {
					// user is resizing height
					StartCoroutine (SetResolution ());
				}
			}
		}
    }

    IEnumerator SetResolution() {
        IsReseting = true;
        Screen.fullScreen = !Screen.fullScreen;
		Screen.SetResolution(Screen.height, Screen.height, false);
        yield return new WaitForSeconds(0.5F);
        IsReseting = false;
    }
}
