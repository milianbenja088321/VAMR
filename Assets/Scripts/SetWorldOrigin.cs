using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class SetWorldOrigin : MonoBehaviour {
    public Transform origin;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        UnityARSessionNativeInterface.GetARSessionNativeInterface().SetWorldOrigin(origin);
        Debug.Log("Changing Origin");
	}
}
