using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StickToCamera : NetworkBehaviour {

    [SerializeField] GameObject  maincamera = null;


	
	// Update is called once per frame
	void Update () {



        if(hasAuthority){
            maincamera = GameObject.FindWithTag("MainCamera");
            transform.position = maincamera.transform.position;
            transform.rotation = maincamera.transform.rotation;
        }
	}
}
