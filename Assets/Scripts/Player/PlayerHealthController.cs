using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveDataExample;

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
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get Data from Save System
        SaveData theSave = SaveSystem.instance.activeSave;

        currentHealth = theSave.currentHealth;
        maxHealth = theSave.maxHealth;

        //Update UIController on Start
        UIController.instance.UpdateHealth(currentHealth, maxHealth);
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
