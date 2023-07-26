using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{

    public static BossHealthController instance;
    public Slider bossHealthSlider;

    public int currentHealth;

    public BossBattle theBoss;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public void TakeDamage (int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;

            //end boss batle
            theBoss.EndBattle();
        }

        bossHealthSlider.value = currentHealth;
    }

}
