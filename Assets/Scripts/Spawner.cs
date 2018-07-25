using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour {
    public List<GameObject> spawnObjects;
    private CustomNetManager netManager;
	private void Start(){
        netManager = FindObjectOfType<CustomNetManager>();
	}

    [Command]
    public void CmdSpawn(GameObject obj){
        Debug.Log("Server Spawner Object Name: " + obj);
        GameObject gameobj = Instantiate(netManager.spawnPrefabs[0]) as GameObject;
        NetworkServer.Spawn(gameobj);
    }

    [Command]
    public void CmdRegister(GameObject obj){
        ClientScene.RegisterPrefab(obj);
    }

    [Command]
    public void CmdDestroy(GameObject obj){
        NetworkServer.Destroy(obj);
    }
}
