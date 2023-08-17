using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgMusic;

    public AudioSource[] soundEffects;

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
        bgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StopBGM()
    {
        bgMusic.Stop();
    }

    public void PlaySFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop(); // stop the sound if it is playing
        soundEffects[sfxNumber].Play(); // play the sound. allows playing sound in fast repetition
    }

    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }
}
