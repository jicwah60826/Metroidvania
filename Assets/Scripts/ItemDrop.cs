using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{


    private Rigidbody2D theRB;

    private bool collected = false;

    private Collider2D theCollider;

    private SpriteRenderer theSR;

    public float forceHigh,forceLow;

    public Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {

        // find it's own rigid body
        theRB = GetComponent<Rigidbody2D>();

        // find it's own Sprite Renderer
        theSR = GetComponent<SpriteRenderer>();

        // find it's own Collision2D
        theCollider = GetComponentInChildren<CircleCollider2D>();

        //calc random force amount
        float forceAmt = Random.Range(forceHigh, forceLow);

        //bounce up in the air as soon as instantiated
        theRB.AddForce(Vector2.up * forceAmt, ForceMode2D.Impulse);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !collected)
        {
   
            collected = true;

            AudioManager.instance.PlaySFXAdjusted(0, .75f, 1.25f, .3f); // Pickup Sound Adjusted

            // deactivate the sprite
            theSR.enabled = false;

            // disable the collider
            theCollider.enabled = false;

            //delete item after 2 seconds
            Destroy(gameObject);
        }

    }
}
