using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject impactEffect; 

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = - transform.up * speed; // we are using -up instead of +right because we rotated the bullet prefab. 
    }

    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        Debug.Log(hitInfo.name);
        // destroy the bullet
        Destroy(gameObject);
        if (hitInfo.gameObject.layer == 12 ) // checks if the bullet hit something that is destructible
        {
            Destroy(hitInfo.gameObject);
            // if yes, then destroy that too
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);

        // TODO: insert sound effect
    }
}
