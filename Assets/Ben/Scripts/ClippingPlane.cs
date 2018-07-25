using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Camera>().nearClipPlane = 0.0001f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
