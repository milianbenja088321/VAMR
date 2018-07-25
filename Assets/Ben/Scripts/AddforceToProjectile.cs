using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddforceToProjectile : MonoBehaviour
{

    Rigidbody mRigidbody;


    private void Awake()
    {
        mRigidbody = this.GetComponent<Rigidbody>();
        mRigidbody.AddForce(this.transform.forward * 4, ForceMode.Impulse);
    }

}
