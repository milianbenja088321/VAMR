using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


	// Use this for initialization
	public int projectileID { get; set; }
    public int coolDown = 2;
    private int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        // if health component doesn't exist in the 
        // object collided then health will return null
        Health health = hit.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }
}
