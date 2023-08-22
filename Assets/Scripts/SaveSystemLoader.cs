using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystemLoader : MonoBehaviour
{

    public SaveSystem systemToLoad;

    private void Awake()
    {
        if (SaveSystem.instance == null)
        {
            Instantiate(systemToLoad).SetupInstance();
        }
    }
}
