using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    //public int totalHealth, healthLow, healthHigh;
    public float totalHealth;

    public GameObject deathEffect;

    public void DamageEnemy(int damageAmount)
    {
        totalHealth -= damageAmount;

        if (totalHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(2); // enemy explode
            }

            Destroy(gameObject);
        }
    }
}
