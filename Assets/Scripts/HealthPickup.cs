using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healAmount;

    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            if (pickupEffect != null)
            {
                PlayerHealthController.instance.HealPlayer(healAmount);
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
}
