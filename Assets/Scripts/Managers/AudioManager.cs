using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource levelMusic;
    public AudioSource mainMenuMusic;
    public AudioSource bossBattleMusic;

    public AudioSource[] soundEffects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void PlayMainMenuMusic()
    {

        if(!mainMenuMusic.isPlaying)
        {
            mainMenuMusic.Play();

            //Stop other music
            levelMusic.Stop();
            bossBattleMusic.Stop();
        }

    }
    
    public void PlayLevelMusic()
    {

        //Start level music only if it's not already playing

        if(!levelMusic.isPlaying)
        {
            levelMusic.Play();

            //Stop other music
            mainMenuMusic.Stop();
            bossBattleMusic.Stop();
        }


    }
    
    public void PlayBossBattleMusic()
    {
        bossBattleMusic.Play();

        //Stop other music
        mainMenuMusic.Stop();
        levelMusic.Stop();
    }

    public void StopBGM()
    {
        levelMusic.Stop();
    }

    public void PlaySFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop(); // stop the sound if it is playing
        soundEffects[sfxNumber].Play(); // play the sound. allows playing sound in fast repetition
    }

    public void PlaySFXAdjusted(int sfxNumberToAdjust, float lowPitch, float hiPitch, float volume)
    {
        soundEffects[sfxNumberToAdjust].pitch = Random.Range(lowPitch, hiPitch);
        soundEffects[sfxNumberToAdjust].volume = volume;
        PlaySFX(sfxNumberToAdjust);
    }

    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }
}
