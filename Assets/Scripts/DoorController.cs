using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

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
    private float fadeWaitTime;
    [SerializeField]
    private string levelToLoad;

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
        distanceToDoor = Vector3.Distance(transform.position, thePlayer.transform.position);

        if (distanceToDoor <= distanceToOpen)
        {
            doorOpen = true;
        }
        else
        {
            doorOpen = false;
        }

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
                thePlayer.canMove = false;
                // start corroutine
                StartCoroutine(useDoorCo());
            }
        }
    }

    IEnumerator useDoorCo()
    {
        playerExiting = true;

        thePlayer.theAnim.enabled = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(fadeWaitTime);

        RespawnController.instance.SetSpawn(exitPoint.position);

        thePlayer.canMove = true;
        thePlayer.theAnim.enabled = true;

        UIController.instance.StartFadeFromBlack();

        SceneManager.LoadScene(levelToLoad);
    }
}
