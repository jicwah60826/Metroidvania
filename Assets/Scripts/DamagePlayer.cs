using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmount = 1;

    public bool destroyOnDamage;
    public GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // this is when the player runs into an object that has a colider
        // EG: the Enemy Walker has a colider that is NOT a trigger
        if (other.gameObject.tag == "Player")
        {
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // for objects that are Triggers that the player enters into
        // EG: the enemy flyer is setup as a trigger
        if (other.tag == "Player")
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        PlayerHealthController.instance.DamagePlayer(damageAmount);

        if (destroyOnDamage)
        {
            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
