using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTarget : MonoBehaviour {

	public void EnableTarget() {
        if(this.gameObject.activeSelf) this.gameObject.SetActive(false);
        else this.gameObject.SetActive(true);
        //this.enabled = !this.enabled;
    }
}
