using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public GameObject playerDeathEffect;

    //[HideInInspector]
    public int currentHealth;
    public int maxHealth;
    public bool isInvincible;

    public float invincibilityLength;
    private float invincCounter;

    public float flashLength;
    private float flashCounter;

    public SpriteRenderer[] playerSprites;

    private void Awake()
    {
        // only load a new instance of this if once doesn't already exist in the scene yet
        if (instance == null)
        {
            instance = this;
            //don't destroy this object when we load scenes or re-load current
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLength;
            }

            if (invincCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0f;
            }
        }

        if (isInvincible)
        {
            currentHealth = maxHealth;
            UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (!isInvincible)
        {
            if (invincCounter <= 0)
            {

                currentHealth -= damageAmount;

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    if (playerDeathEffect != null)
                    {
                        Instantiate(playerDeathEffect, transform.position, transform.rotation);
                    }

                    RespawnController.instance.ReSpawn();
                }
                else
                {
                    invincCounter = invincibilityLength;
                }

                UIController.instance.UpdateHealth(currentHealth, maxHealth);
            }
        }

    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}
