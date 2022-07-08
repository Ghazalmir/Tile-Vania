using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config params
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    // state
    bool isAlive = true;

    // cached references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    BoxCollider2D feet;
    float gravity;




    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        feet = GetComponent<BoxCollider2D>();

        gravity = myRigidBody.gravityScale;
        
    }

    void Update()
    {
        if (!isAlive) {return;}

        Run();
        Jump();
        climbLadder();
        flipSprite();
        Die();
    }

    public void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); // between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    /**
    * flips the player when moving horizontally
    */
    private void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
        
    }

    public void Jump()
    {
        // sigle jumping: 
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
        
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelociyToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelociyToAdd;
        }

        
    }

    public void climbLadder()
    {
        
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravity;
            return;
        }

        myRigidBody.gravityScale = 0f;
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = playerVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);


    }

    private void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();


        }
    }

}
