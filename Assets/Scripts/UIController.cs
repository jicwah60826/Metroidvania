using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public TMP_Text ammoText;
    public TMP_Text bombText;
    public TMP_Text lootText;
    public TMP_Text onScreenLowerThirdText;
    public Slider healthSlider;
    public string mainMenuScene;

    public GameObject pauseScreen;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //disable text as needed
        onScreenLowerThirdText.gameObject.SetActive(false);
        bombText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void UpdateAmmo(int ammoCount)
    {
        ammoText.text = "AMMO: " + ammoCount.ToString();
    }

    public void UpdateBombs(int bombCount)
    {
        bombText.text = "BOMBS: " + bombCount.ToString();
    }

    public void PauseUnPause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        // set time scale back to 1 so that when we start a new game or continue, we're not frozen from a previous pause
        Time.timeScale = 1f;


        // clean the scene and destroy all the game objects that are don't destroy on load.

        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        instance = null;
        Destroy(gameObject);

        //take us to the main menu

        SceneManager.LoadScene(mainMenuScene);
    }
}
