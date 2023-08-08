using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float totalHealth;

    public GameObject deathEffect;

    public GameObject[] itemDrops;

    public float xHigh, xLow;
    public float yHigh, yLow;

    public void DamageEnemy(int damageAmount)
    {
        totalHealth -= damageAmount;

        if (totalHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(2); // enemy explode
                ItemDrop();
            }

            Destroy(gameObject);
        }
    }

    private void ItemDrop()
    {
        for(int i = 0; i < itemDrops.Length; i++)
        {
            //get random values to use on X when spawning items in
            float xSpawn = Random.Range(xHigh, xLow);
            float ySpawn = Random.Range(yHigh, yLow);

            // spawn item drops
            Instantiate(itemDrops[i], transform.position + new Vector3(xSpawn, ySpawn, 0), Quaternion.identity);
        }
    } 
}
