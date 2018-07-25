using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Shoot : NetworkBehaviour {
    public GameObject ammo;
    public float speed;
    //private Text text;
	// Use this for initialization
	void Start () {
         //text = GameObject.FindGameObjectWithTag("SpeedUI").GetComponent<Text>();
        //.text = "Speed: " + speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            Debug.Log("Shooting!");
            ShootBullet();
        }
	}

    void ShootBullet() {
        GameObject bullet = Instantiate(ammo, transform.position, transform.rotation);
        //bullet.transform.parent = this.transform;
        bullet.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    [Command]
    public void CmdIncreaseSpeed() {
        RpcIncreaseSpeed();
    }
    [Command]
    public void CmdDecreaseSpeed() {
        RpcDecreaseSpeed();
    }

    [ClientRpc]
    public void RpcIncreaseSpeed() {
        speed += 1;
        //text.text = "Speed: " + speed;
    }

    [ClientRpc]
    public void RpcDecreaseSpeed() {
        speed -= 1;
        //text.text = "Speed: " + speed;
    }

}
