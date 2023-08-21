using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public Fader fader;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float distanceToOpen;
    
    private float distanceToDoor;
    private bool doorOpen;
    
    [SerializeField]
    private Transform exitPoint;
    
    [SerializeField]
    private float movePlayerSpeed;
    
    
    [SerializeField]
    private int levelToLoad;

    private PlayerController thePlayer;
    private bool playerExiting;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // check player distance to door
        distanceToDoor = Vector3.Distance(transform.position, thePlayer.transform.position);

        if (distanceToDoor <= distanceToOpen)
        {
            doorOpen = true;
        }
        else
        {
            doorOpen = false;
        }

        // do door opening animation
        anim.SetBool("doorOpen", doorOpen);

        if (playerExiting)
        {
            //move player towards the exit point
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!playerExiting)
            {
                playerExiting = true;

                // disable player movement
                thePlayer.canMove = false;

                // disable collider on door trigger
                GetComponent<BoxCollider2D>().enabled = false;

                //set level var
                fader.SetLevel(levelToLoad);


                UpdateSaveSystem();

                //Save Data to Disk
                SaveSystem.instance.Save();

             }
        }
    }

    void UpdateSaveSystem()
    {
        if (playerExiting)
        {

            //Store all player controller data so we can inject into save system
            PlayerController player = PlayerController.instance;

            //Store all player health data so we can inject into save system
            PlayerHealthController playerHealth = PlayerHealthController.instance;

            // Update Save system data
            SaveSystem.instance.activeSave.level = levelToLoad;
            SaveSystem.instance.activeSave.playerSpawnX = exitPoint.transform.position.x;
            SaveSystem.instance.activeSave.playerSpawnY = exitPoint.transform.position.y;
            SaveSystem.instance.activeSave.ammoCount = player.ammoCount;
            SaveSystem.instance.activeSave.bombCount = player.bombCount;
            SaveSystem.instance.activeSave.currentHealth = playerHealth.currentHealth;
            SaveSystem.instance.activeSave.maxHealth = playerHealth.maxHealth;


        }
    }
}
