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

    public float deltaTime;
    // Use this for initialization
    void Start()
    {
        //WebCamDevice[] devices = WebCamTexture.devices;
        //Debug.Log("Number of web cams connected: " + devices.Length);

        //for (int i = 0; i < devices.Length; i++)
        //{
        //    Debug.Log(i + " " + devices[i].name);
        //}

        //Renderer rend = this.GetComponentInChildren<Renderer>();

        //WebCamTexture mycam = new WebCamTexture(1920,1080);
        //string camName = devices[3].name;
        //Debug.Log("The webcam name is " + camName);
        //mycam.deviceName = camName;
        //rend.material.mainTexture = mycam;
        //cam = mycam;
        //mycam.Play();
        Application.targetFrameRate = 60;
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("Number of web cams connected: " + devices.Length);
        Renderer rend = this.GetComponentInChildren<Renderer>();

        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(i + ": " + devices[i].name);
        }
        WebCamTexture mycam = new WebCamTexture(1920, 1080, 60);

        string camName = devices[2].name;
        Debug.Log("The webcam name is " + camName);

        mycam.deviceName = camName;
        rend.material.mainTexture = mycam;
        mycam.Play();
        cam = mycam;
    }

    private void Update()
    {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        Debug.Log(Mathf.Ceil(fps).ToString());

        Debug.Log("Width: " + cam.width + " " + "Height: " + cam.height);

    }
}



