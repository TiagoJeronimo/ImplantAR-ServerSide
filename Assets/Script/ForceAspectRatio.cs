using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour {

	private int lastWidth = Screen.width;

	void Start()
	{
		Screen.SetResolution(750, 750, false);
	}
	void Update()
	{
		if (Screen.width != lastWidth) {
			// user is resizing width
			Screen.SetResolution(Screen.width, Screen.width, false);
			lastWidth = Screen.width;
		} else {
			// user is resizing height
			Screen.SetResolution(Screen.height, Screen.height, false);
		}
	}
}
