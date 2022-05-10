using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private Rigidbody2D rigidBody;
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if (knockBackCounter <= 0)
        {

            rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            //if (isGrounded)
            //{
            //    canDoubleJump = true;
            //}

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                    canDoubleJump = true;
                }
                else
                {
                    if (canDoubleJump)
                    {
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                        canDoubleJump = false;
                    }
                }
            }

            if (rigidBody.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (rigidBody.velocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        } else
        {
            knockBackCounter -= Time.deltaTime;

            rigidBody.velocity = new Vector2(spriteRenderer.flipX ? knockBackForce : -knockBackForce, rigidBody.velocity.y);

        }

        animator.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("isGrounded", isGrounded);

    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        rigidBody.velocity = new Vector2(0f, knockBackForce);

        animator.SetTrigger("hurt");
    }

    public void Bounce()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);
    }

}
