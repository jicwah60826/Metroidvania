using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour

{

    private Animator theAnim;
    private int levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        // get the Animator component that is attached to this game object
        theAnim = GetComponent<Animator>();
    }

    public void SetLevel(int level)
    {
        levelToLoad = level;
        theAnim.SetTrigger("Fade");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
