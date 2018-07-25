using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVelocity : MonoBehaviour
{

    Rigidbody rig;
    public Vector3 lastPosition;
    public Vector3 velocity;
    public float velX;
    // Use this for initialization
    void Start()
    {
        Debug.Log("DebugVelocity::Start()");
        rig = gameObject.GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rig = this.gameObject.GetComponent<Rigidbody>();
        velX = transform.position.x - lastPosition.x; 
        velocity = transform.position - lastPosition; 
        //Debug.Log(velocity);
        lastPosition = transform.position;
        
    }
}
