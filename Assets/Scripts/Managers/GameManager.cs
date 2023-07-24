using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Hide / Lock cursor movement in game window
        Cursor.lockState = CursorLockMode.Locked;

        //Set Cursor to not be visible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quitting Application");
            Application.Quit();
        }
    }
}
