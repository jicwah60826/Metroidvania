using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public Rigidbody2D theRB;

    public float moveSpeed;
    public float jumpForce;

    //Coyote time
    public float hangTime = .2f;
    public float smallJumpMult = .5f;
    [SerializeField]
    private float hangCounter;


    public Transform groundPoint;
    [SerializeField]
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator theAnim;

    public BulletController shotToFire;
    public Transform shotPoint;

    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private bool canDoubleJump;
    [SerializeField]
    private bool isDoubleJumping;
    [SerializeField]
    private bool canTripleJump;


    public float dashSpeed, dashTime;
    private float dashCounter;

    public SpriteRenderer theSR, afterImage;
    public float afterImageLifetime, timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterDashing;
    private float dashRechargeCounter;

    public GameObject standing, ball;
    public float waitToBall;
    private float ballCounter;
    public Animator ballAnim;

    public Transform bombPoint;
    public GameObject bomb;

    public int ammoCount, bombCount;
    public bool infinteAmmo, infiniteBombs;

    private PlayerAbilityTracker playerAbilities;

    public bool canMove;
    private bool isDashing;

    private void Awake()
    {
        // only load a new instance of this if once doesn't already exist in the scene yet
        if (instance == null)
        {
            instance = this;
            //don't destroy this object when we load scenes or re-load current
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAbilities = GetComponent<PlayerAbilityTracker>();

        canMove = true;

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



        // only do all of this if we can move and time is not paused
        if (canMove && Time.timeScale != 0)
        {

            if (dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;
                isDashing = false;
                //theAnim.SetBool("isDashing", false);
            }
            else
            {

                if (Input.GetButtonDown("Fire2") && standing.activeSelf && playerAbilities.canDash)
                {
                    dashCounter = dashTime;

                    isDashing = true;
                    AudioManager.instance.PlaySFX(3); // single jump sound
                }
            }

            if (dashCounter > 0)
            {
                dashCounter = dashCounter - Time.deltaTime;

                theRB.velocity = new Vector2(dashSpeed * transform.localScale.x, theRB.velocity.y);

                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0)
                {
                    isDashing = true;
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
                hangCounter = hangTime;
            }
            else 
            {
                hangCounter -= Time.deltaTime;
            }

            // Handle Jumping
            if (Input.GetButtonDown("Jump") && (hangCounter >0 || (canDoubleJump && playerAbilities.canDoubleJump) || (canTripleJump && playerAbilities.canTripleJump)))
            {
                isJumping = true;

                if (isOnGround)
                {
                    canDoubleJump = true;
                    canTripleJump = false;
                    isDashing = false;
                    AudioManager.instance.PlaySFX(12); // single jump sound
                }


                // allow double jump
                if (!isOnGround && canDoubleJump)
                {
                    canTripleJump = true;
                    canDoubleJump = false;
                    isDashing = false;
                    isDoubleJumping = true;
                    theAnim.SetTrigger("doubleJump");
                    AudioManager.instance.PlaySFX(13); // double jump sound
                }

                // allow triple jump
                if (!isOnGround  && isDoubleJumping)
                {
                    canDoubleJump = false;
                    isDoubleJumping = false;
                    canTripleJump = false;
                    isDashing = false;
                    theAnim.SetTrigger("doubleJump");
                    AudioManager.instance.PlaySFX(13); // triple jump sound
                }
                else
                {
                    canTripleJump = false;
                    isDashing = false;
                    isDoubleJumping = false;
                }

                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

            // Allow small jumps

            if (!isOnGround)
            {
                
                if (Input.GetButtonUp("Jump") && theRB.velocity.y > 0)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * smallJumpMult);
                }
            }

            //shooting
            if (Input.GetButtonDown("Fire1") && !isDoubleJumping)
            {
                if (standing.activeSelf && (ammoCount > 0 || infinteAmmo))
                {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0f);
                    AudioManager.instance.PlaySFX(8);
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

                        //AudioManager.instance.PlaySFX(6);
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

                        //AudioManager.instance.PlaySFX(10);
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

    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifetime);

        afterImageCounter = timeBetweenAfterImages;
    }
}
