using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using UnityEngine;

public class CustomNetManager : NetworkManager
{
    //[SerializeField] Button gameButton;

    [SerializeField] private int prefabIndex = 0;
    //public List<GameObject> spawnObjects;

    public void prefabPicker(string buttonName)
    {
        switch (buttonName)
        {
            case "Host":
                prefabIndex = 0;
                break;
            case "Client":
                prefabIndex = 1;
                break;
        }
    }

    public void JoinGame()
    {
        NetworkManager.singleton.networkAddress = "192.168.0.7";
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartClient();
    }

    public void StartGame()
    {
        NetworkManager.singleton.StartHost();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnClientConnect:: Called");
        base.OnClientConnect(conn);

        //if (gameButton != null)
        //    gameButton.gameObject.SetActive(false);

        IntegerMessage msg = new IntegerMessage(prefabIndex);

        if (!clientLoadedScene)
        {
            if (!ClientScene.ready)
                ClientScene.Ready(conn);

            if (autoCreatePlayer)
            {
                ClientScene.AddPlayer(conn, 0, msg);
            }
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        base.OnServerAddPlayer(conn, playerControllerId);

        int id = -1;

        if (extraMessageReader != null)
        {
            IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
            id = i.value;
        }
        Debug.Log("OnServerAddPlayer:: Spawn ID: " + id);

        GameObject playerPrefab = spawnPrefabs[id];

        GameObject player;

        Transform startPos = GetStartPosition();

        if (startPos != null)
        {
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

}
