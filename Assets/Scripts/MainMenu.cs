using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int levelToLoad;

    private void Start()
    {
        AudioManager.instance.PlayMainMenuMusic();
    }


    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
        AudioManager.instance.PlayLevelMusic();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
