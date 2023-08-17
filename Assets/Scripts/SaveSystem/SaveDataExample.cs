using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SaveDataExample
{

    public string text;
    public float number;
    public int wholeNumber;

    public List<Vector3> places;

    public string[] Strings;

    public enum CurrentLevel { Area1, Area2, Area3, Boss1};
    public CurrentLevel currentLevel;

    public SubSave moreData;
}

[System.Serializable]
public class SubSave

{
    public string someText;
    public bool engineActive;
}
