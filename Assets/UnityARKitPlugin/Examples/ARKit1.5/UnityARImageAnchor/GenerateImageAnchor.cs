using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.Networking;
using System;

public class GenerateImageAnchor : MonoBehaviour {

    public bool generateReflectionProbe = false;
	[SerializeField]
	private ARReferenceImage referenceImage;

	[SerializeField]
	private GameObject prefabToGenerate;

	private GameObject imageAnchorGO;
    public Spawner spawner;

    private GameObject playArea;

	// Use this for initialization
	void Start () {
		UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
		UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
		UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += RemoveImageAnchor;
        playArea = GameObject.FindGameObjectWithTag("Play Area");
        if (playArea)
            Debug.Log("Found Play Area in scene");
        else Debug.Log("Did not find Play Area in scene");
        //playArea.SetActive(false);

	}

    void CreateEnvironmentProbe(Matrix4x4 worldTransform)
    {
        Debug.Log("Adding Environment Probe");
        //note we have not converted to Unity coord system yet, so we can pass it in directly
        UnityAREnvironmentProbeAnchorData anchorData;
        anchorData.cubemapPtr = IntPtr.Zero;
        anchorData.ptrIdentifier = IntPtr.Zero;
        anchorData.probeExtent = Vector3.one;
        anchorData.transform = UnityARMatrixOps.GetMatrix(worldTransform); //this should be in ARKit coords
        anchorData = UnityARSessionNativeInterface.GetARSessionNativeInterface().AddEnvironmentProbeAnchor(anchorData);
        Debug.Log("Finished Adding Environment Probe: " + anchorData.transform);
    }

	void AddImageAnchor(ARImageAnchor arImageAnchor)
	{
   		Debug.Log ("image anchor added");
		if (arImageAnchor.referenceImageName == referenceImage.imageName) {
			Vector3 position = UnityARMatrixOps.GetPosition (arImageAnchor.transform);
			Quaternion rotation = UnityARMatrixOps.GetRotation (arImageAnchor.transform);
            //playArea.SetActive(true);
            if (playArea)
            {
                playArea.transform.position = position;
                playArea.transform.rotation = rotation;
                UnityARSessionNativeInterface.GetARSessionNativeInterface().SetWorldOrigin(playArea.transform);
                UnityARSessionNativeInterface.GetARSessionNativeInterface().AddUserAnchorFromGameObject(playArea);
                if(generateReflectionProbe)
                    CreateEnvironmentProbe(UnityARMatrixOps.UnityToARKitCoordChange(position, rotation));
            }
            else{
                Debug.Log("Tried to add Image Anchor, but Play Area null");
            }
            //imageAnchorGO = Instantiate(prefabToGenerate, position, rotation) as GameObject;
            //UnityARSessionNativeInterface.GetARSessionNativeInterface().SetWorldOrigin(imageAnchorGO.transform);
            //UnityARSessionNativeInterface.GetARSessionNativeInterface().AddUserAnchorFromGameObject(imageAnchorGO);
            //CmdSpawn(imageAnchorGO);
		}
	}

    IEnumerator Spawn(GameObject obj){
        yield return new WaitForSeconds(2);
        //Debug.Log("Spawn Coroutine Object Name: " + obj.name);
        ////GameObject obj = GameObject.Find(objname);
        //if (spawner)
        //{
        //    Debug.Log("The spawner file is not null");
        //}
        //else Debug.Log("The spawner file does not exist");
        //spawner.CmdSpawn(obj);
    }

	void UpdateImageAnchor(ARImageAnchor arImageAnchor)
	{
		if (arImageAnchor.referenceImageName == referenceImage.imageName) {
            //playArea.transform.position = UnityARMatrixOps.GetPosition (arImageAnchor.transform);
            //playArea.transform.rotation = UnityARMatrixOps.GetRotation (arImageAnchor.transform);
		}

	}

	void RemoveImageAnchor(ARImageAnchor arImageAnchor)
	{   
		Debug.Log ("image anchor removed");
		if (imageAnchorGO) {
			GameObject.Destroy (imageAnchorGO);
		}

	}

	void OnDestroy()
	{
		UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;
		UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent -= UpdateImageAnchor;
		UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveImageAnchor;

	}

	// Update is called once per frame
	void Update () {
        if (!spawner)
        {
            spawner = FindObjectOfType<Spawner>();
        }
	}
}
