using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D theRB;
    public Vector2 moveDirection;

    private void Update()
    {
        theRB.velocity = moveDirection * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
