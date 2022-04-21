using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rigidBody;
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
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

        animator.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("isGrounded", isGrounded);

    }
}