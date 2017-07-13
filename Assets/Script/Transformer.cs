using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.position = Server.Position;
        transform.rotation = Server.Rotation;
	}
}
