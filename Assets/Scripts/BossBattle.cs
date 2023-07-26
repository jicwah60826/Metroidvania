using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossBattle : MonoBehaviour

{

    private CameraController theCam;
    public Transform camPosition;
    public float camMoveSpeed;

    public int threshold1;
    public int threshold2;

    public float activeTime, fadeOutTime, inactiveTime;

    private float activeCounter, fadeCounter, inactiveCounter;

    public Transform[] spawnPoints;

    private Transform targetPoint;

    public float moveSpeed;

    public Animator anim;

    public Transform theBoss;



    // Start is called before the first frame update
    void Start()
    {

        activeCounter = activeTime;

        theCam = FindObjectOfType(typeof(CameraController)) as CameraController;

        //disable the camera controller script on theCam so that we can move the camera as needed
        theCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camMoveSpeed * Time.deltaTime);


        /////////////////// ************* PHASE / THRESHHOLD 1 *********** //////////////////

        if (BossHealthController.instance.currentHealth > threshold1)
        {
            if (activeCounter > 0)
            {
                // Boss is now active - start active countdown timer
                activeCounter -= Time.deltaTime;

                if (activeCounter <= 0)
                {
                    // Boss active time is up - trigger vanish animation and start fade clock counter
                    fadeCounter += fadeOutTime;
                    anim.SetTrigger("vanish");
                }
            }
            else if (fadeCounter > 0)
            {
                // once fade counter reaches fadeOuttime - disable boss and re-initialize inactiveCounter
                fadeCounter -= Time.deltaTime;
                if (fadeCounter < 0)
                {
                    theBoss.gameObject.SetActive(false);
                    inactiveCounter = inactiveTime;
                }
            }
            else if (inactiveCounter > 0)
            {
                // Ghosr is now inactive - start inactive counter
                inactiveCounter -= Time.deltaTime;

                if (inactiveCounter <= 0)
                {
                    // once inactive time passes - move the boss to a new random spawn point directly
                    theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    theBoss.gameObject.SetActive(true);

                    // Re-acticate boss at new random spawn point
                    activeCounter = activeTime;
                }
            }
        }

        /////////////////// ************* PHASE / THRESHHOLD 2 *********** //////////////////

        else
        {
            if (targetPoint == null)
            {
                targetPoint = theBoss;
                fadeCounter = fadeOutTime;
                anim.SetTrigger("vanish");
            }
            else
            {
                if (Vector3.Distance(theBoss.position, targetPoint.position) > .02f)
                {
                    theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, moveSpeed * Time.deltaTime);


                    if (Vector3.Distance(theBoss.position, targetPoint.position) <= .02f)
                    {
                        fadeCounter = fadeOutTime;
                        anim.SetTrigger("vanish");
                    }

                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        theBoss.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                        targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                        int whileBreaker = 0;
                        while (targetPoint.position == theBoss.position && whileBreaker < 100)
                        {
                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                            whileBreaker++;
                        }

                        theBoss.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void EndBattle()
    {
        gameObject.SetActive(false);
    }
}
