using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Sirenix.OdinInspector;


public class EnemyPatroller : MonoBehaviour
{

    public bool isPatrolling;

    public Transform[] patrolPoints;

    private int currentPoint;

    public float waitTimeHigh, waitTimeLow, moveSpeed, chaseSpeed;

    private float waitAtPointTime, waitCounter;

    public float jumpForce;

    public Rigidbody2D theRB;

    public Animator anim;

    public float rangeToStartChase;

    [SerializeField]
    private bool isChasing;

    private Transform player;




    private bool playerInRange;

    // vars for wall touching detection
    [SerializeField]
    private Transform frontCheck;
    //[SerializeField]
    private bool isTouchingWall;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    public LayerMask whatIsWall;



    // Start is called before the first frame update
    void Start()
    {

        //get player
        player = PlayerHealthController.instance.transform;

        //randomly generate a start waiting time
        waitAtPointTime = Random.Range(waitTimeLow, waitTimeHigh);

        // initiate the waitCounter and the randomly generated time
        waitCounter = waitAtPointTime;

        // unparent the pPoints from the walker object
        foreach (Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            // are we horizontally at the same position of a patrol point?
            if (
                Mathf
                    .Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f
            )
            {
                //move LEFT towards current point
                if (transform.position.x < patrolPoints[currentPoint].position.x
                )
                {
                    theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

                    //flip player sprite when moving to left
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    //move RIGHT towards current point
                    theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

                    //flip player sprite when moving to left
                    transform.localScale = Vector3.one;
                }

                //if patroller NOT jumping and MOT on the same Y level as point - make enemy jump
                if (transform.position.y < patrolPoints[currentPoint].position.y - .5f && theRB.velocity.y < .1)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                }

            }
            else
            //patroller is now at the same position of his current patrol point
            {
                // stop walking on X
                theRB.velocity = new Vector2(0f, theRB.velocity.y);

                // start the wait counter clock
                waitCounter -= Time.deltaTime;

                if (waitCounter <= 0)
                {
                    //randomly generate a start waiting time
                    waitAtPointTime = Random.Range(waitTimeLow, waitTimeHigh);

                    // reset wait Counter clock once countdown stops
                    waitCounter = waitAtPointTime;

                    //iterate to the next point in the array
                    currentPoint++;

                    //protect against going out of array bounds
                    if (currentPoint >= patrolPoints.Length)
                    {
                        currentPoint = 0;
                    }
                }
            }

            //animations
            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));

            // Is touching wall check
            isTouchingWall = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsWall);

            if (isTouchingWall)
            {
                //make enemy jump / crawl wall until over
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
        }


        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, player.position) < rangeToStartChase)
            {
                isChasing = true;
                Debug.Log("isChasing!");
                //theAnim.SetBool("isChasing", isChasing);
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
        }
    }

}
