using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{

    string camName = "THETA UVC FullHD Blender";
    int cameraIndex;
    [SerializeField] Material test = null;
 
    WebCamDevice[] devices;
    WebCamTexture cam;

    // Use this for initialization
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("Number of web cams connected: " + devices.Length);
        Renderer rend = this.GetComponentInChildren<Renderer>();

        WebCamTexture mycam = new WebCamTexture();
        string camName = devices[2].name;
        Debug.Log("The webcam name is " + camName);
        mycam.deviceName = camName;
        rend.material.mainTexture = mycam;
        mycam.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) { }
    }

    void PlayCamera()
    {
        cam.Play();
    }

    void StopCamera()
    {
        cam.Stop();
    }
}
