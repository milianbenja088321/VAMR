using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{

    string camName = "THETA UVC FullHD Blender";
    public int cameraIndex = 1;

    WebCamDevice[] devices;
    WebCamTexture cam;

    public float deltaTime;
    // Use this for initialization


    void Start()
    {

        //Application.targetFrameRate = 60;
        // Application.RequestUserAuthorization(UserAuthorization.WebCam);
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("Number of web cams connected: " + devices.Length);
        Renderer rend = this.GetComponentInChildren<Renderer>();

        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(i + ": " + devices[i].name);
        }
        WebCamTexture mycam = new WebCamTexture(100, 100);

        string camName = devices[cameraIndex].name;
        Debug.Log("The webcam name is " + camName);

        mycam.deviceName = camName;
        mycam.requestedFPS = 60;
        rend.material.mainTexture = mycam;
        mycam.mipMapBias = 2;
        mycam.Play();
        Debug.Log("Width: " + mycam.width);
        Debug.Log("Current Texture Memory: " + Texture.currentTextureMemory);
        Debug.Log("Streaming Texture Count: " + Texture.streamingTextureCount);
        //  mycam.Play();
        //cam = mycam;
    }

    private void Update() {
        
    }
}



