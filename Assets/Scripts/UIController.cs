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
    public TMP_Text onScreenLowerThirdText;
    public Slider healthSlider;

    public Image fadeScreen;

    public float fadeSpeed = 2f;
    private bool fadingToBlack,
        fadingFromBlack;

    public string mainMenuScene;

    public GameObject pauseScreen;

    private void Awake()
    {
        instance = this;

        //// only load a new instance of this if once doesn't already exist in the scene yet
        //if (instance == null)
        //{
        //    instance = this;
        //    //don't destroy this object when we load scenes or re-load current
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //disable text as needed
        onScreenLowerThirdText.gameObject.SetActive(false);
        bombText.gameObject.SetActive(false);
        bombText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingToBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime)
            );

            if (fadeScreen.color.a == 1f)
            {
                fadingToBlack = false;
            }
        }
        else if (fadingFromBlack)
        {
            fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime)
            );

            if (fadeScreen.color.a == 0f)
            {
                fadingFromBlack = false;
            }
        }

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

    public void StartFadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        fadingFromBlack = true;
        fadingToBlack = false;
    }

    public void PauseUnPause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;

            // Hide / Lock cursor movement in game window
            Cursor.lockState = CursorLockMode.Locked;

            //Set Cursor to not be visible
            Cursor.visible = false;
        }
        else
        {
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;

            // Show / un-lock cursor movement in game window
            Cursor.lockState = CursorLockMode.None;

            //Set Cursor to be visible
            Cursor.visible = true;
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
