using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeToExplode = .5f;

    public GameObject explosion;

    public GameObject destructibleEffect;

    public float blastRange;

    public LayerMask whatIsDestructible;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeToExplode -= Time.deltaTime;
        if (timeToExplode <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(15); // explosion sfx
            }
            Destroy(gameObject);

            // Get all objects within the overlap circle and add to objectsToDamage array
            Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructible);

            // if array has objects, destroy all within blast range
            if (objectsToRemove.Length > 0)
            {
                foreach (Collider2D col in objectsToRemove)
                {
                    Instantiate(destructibleEffect, col.gameObject.transform.position, col.gameObject.transform.rotation);
                    Destroy(col.gameObject);
                }
            }
        }
    }
}
