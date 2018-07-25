using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField] GameObject refToController;
    [SerializeField] float force = 50f;

    SteamVR_TrackedController controller;

    Vector3 lastPosition;
    Vector3 velocity;


    void Start()
    {

        controller = refToController.GetComponent<SteamVR_TrackedController>();
    }

    void FixedUpdate()
    {
        velocity = transform.position - lastPosition;

        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Collision::() Projectile collided with " + this.gameObject.name);

            SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse(5000);

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddForce(velocity * force, ForceMode.VelocityChange);
                rb.useGravity = true;
            }
            else
            {
                Debug.Log("There is no rigidbody in " + collision.gameObject.name);
            }
        }
    }

}
