using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour
{
    [SerializeField] private GameObject projObject = null;
    [SerializeField] private Transform projectileSpawn;

    private Projectile theBullet = null;
    public float timer;
    private GameObject projectile;
    public bool didShoot;

    void Update()
    {
        if (!isLocalPlayer) return;

        //GameObject thePlayer = GameObject.FindGameObjectWithTag("MainCamera");
        //this.transform.position = thePlayer.transform.position;
        //this.transform.rotation = thePlayer.transform.rotation;
        GameObject ARCam = GameObject.Find("ARCamera");

        if (ARCam != null)
        {
            this.transform.position = ARCam.transform.position;
            this.transform.rotation = ARCam.transform.rotation;

            if (Input.GetKeyDown(KeyCode.Space)) //Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CmdShoot();
            }
        }
        else
        {
            var x = Input.GetAxis("Horizontal") * 0.1f;
            var z = Input.GetAxis("Vertical") * 0.1f;

            transform.Translate(x, 0, z);
        }

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GetComponent<MeshRenderer>().material.color = Color.red;
    }


    #region COMMANDS
    [Command]
    public void CmdShoot()
    {
        RpcShoot();
    }
    #endregion


    #region RPCs
    [ClientRpc]
    public void RpcShoot()
    {
        // Client side RPC
        Debug.Log("RpcShoot:: Called in the network");

        projectile = Instantiate(
            projObject,
            projectileSpawn.position,
            projectileSpawn.rotation
            );

        theBullet = projectile.GetComponent<Projectile>();
        theBullet.projectileID++;

        Debug.Log("CmdShoot:: Projectile ID: " + theBullet.projectileID);

        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 6;
        projectile.transform.SetParent(null);

        NetworkServer.Spawn(projectile);

        // Call RPC for because this command only wokrs for server.

        Destroy(projectile, 2.0f);
    }
    #endregion

}
