using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;

    public Rigidbody2D theRB;

    public Vector2 moveDirection;

    public GameObject ImpactEffect;

    public int damageAmount;

    private void Update()
    {
        theRB.velocity = moveDirection * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);
            }

            if (other.tag == "Boss")
            {
                BossHealthController.instance.TakeDamage(damageAmount);
            }

            Instantiate(ImpactEffect, transform.position, Quaternion.identity);
            // AudioManager.instance.PlaySFX(10); // bullet impact

            AudioManager.instance.PlaySFXAdjusted(10, .75f, 1.25f, 1f); // Impact Sound Adjusted

            Destroy (gameObject);
        }

        if (!GetComponent<Renderer>().isVisible)
        {
            OnBecameInvisible();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy (gameObject);
    }
}
