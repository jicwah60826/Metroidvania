using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMLoader : MonoBehaviour
{

    public AudioManager theAM;

    private void Awake()
    {
        if(AudioManager.instance == null)
        {
            
            AudioManager newAM = Instantiate(theAM);
            AudioManager.instance = newAM;
            DontDestroyOnLoad(newAM.gameObject);
        }
    }
}
