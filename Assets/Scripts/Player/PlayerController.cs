using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public Rigidbody2D theRB;

    public float moveSpeed;

    //[Title("Ammo")]
    public int ammoCount;
    public bool infinteAmmo;
    public BulletController shotToFire;
    public Transform shotPoint;

    //[Title("Jumping")]
    public float jumpForce;
    public int maxJumps;
    [ShowInInspector]
    [ReadOnly]
    private int jumpsLeft;
    [ShowInInspector]
    [ReadOnly]
    private int jumpCounter;
    //[Title("Coyote Time")]
    [SerializeField]
    public float hangTime = .2f;
    //[Title("Jump Buffer")]
    [SerializeField]
    public float smallJumpMult = .5f;
    [SerializeField]
    private float hangCounter;
    [SerializeField]
    private bool isDoubleJumping;


    //[Title("Ground")]
    public Transform groundPoint;
    [ShowInInspector]
    [ReadOnly]
    private bool isOnGround;
    [ShowInInspector]
    [ReadOnly]
    private bool wasOnGround;
    public LayerMask whatIsGround;



    //[Title("Wall Detection")]
    [SerializeField]
    private Transform frontCheck;
    [ShowInInspector]
    [ReadOnly]
    private bool isTouchingWall;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    public LayerMask whatIsWall;



    //[Title("Dash")]
    public float dashSpeed;
    public float dashTime;
    public float dashHangAmt;
    private float dashCounter;
    public float waitAfterDashing;
    private float dashRechargeCounter;
    private bool isDashing;

    private float afterImageCounter;

    //[Title("Ball")]
    public GameObject standing;
    public GameObject ball;
    public float waitToBall;
    private float ballCounter;
    public Animator ballAnim;

    //[Title("Bomb")]
    public int bombCount;
    public Transform bombPoint;
    public GameObject bomb;
    public bool infiniteBombs;

    //[Title("Misc")]
    private PlayerAbilityTracker playerAbilities;
    public bool canMove;
    public Animator theAnim;

    private void Awake()
    {


        instance = this;


        //// only load a new instance of this if once doesn't already exist in the scene yet
        //if (instance == null)
        //{
        //    instance = this;
        //    //don't destroy this object when we load scenes or re-load current
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

    }

    // Start is called before the first frame update
    void Start()
    {
        playerAbilities = GetComponent<PlayerAbilityTracker>();

        canMove = true;

        // Determine what jump abiliy we have

        // intialize
        maxJumps = 1;

        if (playerAbilities.canDoubleJump)
        {
            maxJumps = 2;
        }
        if (playerAbilities.canTripleJump)
        {
            maxJumps = 3;
        }

        // Initialize how many jumps we can do
        jumpsLeft = maxJumps;
        jumpCounter = 0;

        // give some ammo and bombs to start if infinite
        if (infinteAmmo)
        {
            ammoCount = 10;
        }
        if (infiniteBombs)
        {
            bombCount = 10;
        }

        UIController.instance.UpdateAmmo(ammoCount);
        UIController.instance.UpdateBombs(bombCount);
    }

    // Update is called once per frame
    void Update()
    {

        // show bombs UI overlay if bombs allowed
        if (playerAbilities.canDropBomb)
        {
            UIController.instance.bombText.gameObject.SetActive(true);
        }


        // Is touching wall check
        isTouchingWall = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsWall);

        // only do all of this if we can move and time is not paused
        if (canMove && Time.timeScale != 0)
        {

            if (dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;
                isDashing = false;
                theAnim.SetBool("isDashing", false);
            }
            else
            {

                if (Input.GetButtonDown("Fire2") && standing.activeSelf && playerAbilities.canDash)
                {
                    dashCounter = dashTime;

                    isDashing = true;

                    //AudioManager.instance.PlaySFXAdjusted(3); // Dash Sound Adjusted

                    AudioManager.instance.PlaySFX(3); // Dash Sound
                }
            }

            if (dashCounter > 0)
            {
                dashCounter = dashCounter - Time.deltaTime;

                theRB.velocity = new Vector2(dashSpeed * transform.localScale.x, theRB.velocity.y * dashHangAmt);

                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0)
                {
                    isDashing = true;
                }

                if (isTouchingWall)
                {
                    isDashing = false;
                }

                dashRechargeCounter = waitAfterDashing;

            }
            else
            {

                //move sideways
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

                //handle direction change
                if (theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (theRB.velocity.x > 0)
                {
                    transform.localScale = Vector3.one;
                }

                isDashing = false;
            }

            //checking if on the ground
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            if (isOnGround)
            {
                // reset stuff
                hangCounter = hangTime;
            }
            else 
            {
                hangCounter -= Time.deltaTime;
            }

            //********** Handle Jumping - SIMPLE *********//

            var jumpInput = Input.GetButtonDown("Jump");

            if (jumpInput)
            {
                jumpCounter += 1;
            }

            if (isOnGround  &&  theRB.velocity.y <= 0)
            {
                jumpsLeft = maxJumps;
            }

            if (jumpInput && jumpsLeft > 0)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                jumpsLeft -= 1;


                //AudioManager.instance.PlaySFXAdjusted(13); // Jump Sound Adjusted

                AudioManager.instance.PlaySFX(13);
            }

            // Allow small jumps
            if (!isOnGround)
            {

                if (Input.GetButtonUp("Jump") && theRB.velocity.y > 0)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * smallJumpMult);
                }
            }

            // Stop dashing if we jump
            if(isDashing && jumpInput)
            {
                isDashing = false;
                //reset gravity scale
            }

            // Double Jumping?


            if(playerAbilities.canDoubleJump && jumpCounter == 2) { 
                isDoubleJumping = true;
            }
            else
            {
                isDoubleJumping = false;
            }

            if (isDoubleJumping)
            {
                theAnim.SetTrigger("doubleJump");
            }

            // reset jumpCounter
            if (jumpCounter > maxJumps)
            {
                jumpCounter = 0;
            }

            //Player was JUST in the air but is now back on the ground
            if(!wasOnGround && isOnGround)
            {
                jumpCounter = 0;
            }


            wasOnGround = isOnGround;

            //shooting
            if (Input.GetButtonDown("Fire1") && !isDashing)
            {
                if (standing.activeSelf && (ammoCount > 0 || infinteAmmo))
                {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0f);
                    //AudioManager.instance.PlaySFX(8);



                    AudioManager.instance.PlaySFXAdjusted(8, .75f, 1.25f, 1f); // Fire Sound Adjusted

                    if (!infinteAmmo)
                    {
                        ammoCount--;
                        UIController.instance.UpdateAmmo(ammoCount);
                    }

                    theAnim.SetTrigger("shotFired");
                }
                else if (ball.activeSelf && playerAbilities.canDropBomb && (bombCount > 0 || infiniteBombs))
                {
                    Instantiate(bomb, bombPoint.position, bombPoint.rotation);

                    if (!infiniteBombs)
                    {
                        bombCount--;
                        UIController.instance.UpdateBombs(bombCount);
                    }
                }
            }

            //ball mode
            if (!ball.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") < -.9f && playerAbilities.canBecomeBall)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);

                        AudioManager.instance.PlaySFX(6);
                    }

                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > .9f)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);

                        AudioManager.instance.PlaySFX(6);
                    }

                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
        }
        else
        {
            // if canMove = false, we cannot move
            theRB.velocity = Vector2.zero;
        }


        if (standing.activeSelf)
        {
            theAnim.SetBool("isOnGround", isOnGround);
            theAnim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
            theAnim.SetBool("isDashing", isDashing);
        }

        if (ball.activeSelf)
        {
            ballAnim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        }
    }
}
