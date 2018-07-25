using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   // [SerializeField] private NetworkedHealth myHealth = null;

    public float phealth;
    public bool isDead;
    private float mHealth = 100;

    private void Start()
    {
        phealth = mHealth;
        isDead = false;
    }

    void Update()
    {
        phealth = mHealth;
        SetPlayerPosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            Debug.Log("Hello Projectile ********************************************");
          //  TakeDamage((int)collision.gameObject.GetComponent<Projectile>().GetDamage());
        }
    }

    private void SetPlayerPosition()
    {
        GameObject head = GameObject.FindGameObjectWithTag("Head");


        if (head)
        {
            Debug.Log("Head not null");
           // this.transform.position = head.transform.position;
            this.transform.rotation = head.transform.rotation;
        }
        else
        {
            Debug.Log("head is null");
        }

    }

    private void TakeDamage(int amount)
    {
        Debug.Log("TakeDamage()::");
        mHealth -= amount;

        if (mHealth <= 0)
        {
            mHealth = 0;
            isDead = true;
        }
    }
}
