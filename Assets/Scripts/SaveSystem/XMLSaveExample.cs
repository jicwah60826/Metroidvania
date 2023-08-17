using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class XMLSaveExample : MonoBehaviour
{


    // use class SaveDataExample that we defined in SaveDataExample.cs
    public SaveDataExample dataToSave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.S)) {

            // save data
            Save();

        }

        if (Input.GetKeyDown(KeyCode.L))
        {

            // load data
            Load();

        }

        if (Input.GetKeyDown(KeyCode.C))
        {

            // clear data
            Clear();

        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            //open save folder
            EditorUtility.RevealInFinder(Application.persistentDataPath);

        }

#endif
    }

    void Save()
    {
        Debug.Log("Saving Data");

        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveDataExample));

        var stream = new FileStream(dataPath + "/testSave.save", FileMode.Create);

        serializer.Serialize(stream, dataToSave);

        stream.Close();

    }

    void Load()
    {
        Debug.Log("Loading Data");

        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/testSave.save"))
        {

            var serializer = new XmlSerializer(typeof(SaveDataExample));

            var stream = new FileStream(dataPath + "/testSave.save", FileMode.Open);

            dataToSave = serializer.Deserialize(stream) as SaveDataExample;

            stream.Close();
        }
        else
        {
            Debug.LogWarning("No save data to load");
        }
    }

    void Clear()
    {
        Debug.Log("Deleted Save File");

        File.Delete(Application.persistentDataPath + "/testSave.save");
    }
}
