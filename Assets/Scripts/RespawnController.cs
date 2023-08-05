using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{

    public static RespawnController instance;

    private Vector3 respawnPoint;
    public float waitToRespawn;

    private GameObject thePlayer;

    private void Awake()
    {
        // only load a new instance of this if once doesn't already exist in the scene yet
        if (instance == null)
        {
            instance = this;
            //don't destroy this object when we load scenes or re-load current
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;
        respawnPoint = thePlayer.transform.position;
    }

    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    public void ReSpawn()
    {
        StartCoroutine(ReSpawnCo());
    }

    IEnumerator ReSpawnCo()
    {

        thePlayer.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);

        //reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        thePlayer.transform.position = respawnPoint;
        thePlayer.SetActive(true);
        PlayerHealthController.instance.FillHealth();
    }
}
