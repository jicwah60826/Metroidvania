using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D theRB;

    public Animator theAnim;

    public float moveSpeed;
    public float jumpForce;

    public BulletController shotToFire;
    public Transform firePoint;

    public LayerMask whatIsGround; // Layer mask that defines WHAT the ground is
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDistance;
    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerAnimations();
        HandleShooting();
    }

    private void PlayerMovement()
    {

        // move sideways
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        // handle direction change
        if (theRB.velocity.x < 0)
        {
            //flip character
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }

        // check if player is on the grouind (if we are within range of any ground layer object)
        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckDistance, whatIsGround);
        Debug.Log("isOnGround = " + isOnGround);

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
    }

    private void PlayerAnimations()
    {
        theAnim.SetFloat("playerSpeed", Mathf.Abs(theRB.velocity.x));
        theAnim.SetBool("isOnGround", isOnGround);
    }

    private void HandleShooting()
    {
        // Handle Shooting - single shots (each shot delayed by fire rate amount)
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(shotToFire, firePoint.position, firePoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0f);
            theAnim.SetTrigger("shotFired");
        }
    }
}
