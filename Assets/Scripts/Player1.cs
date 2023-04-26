using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    // Movement
    private float horizontal;
    static public float speed = 8f;
    private float jumpPower = 28f;
    private bool isFacingRight = true;

    // Dash
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 32f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 5f;

    // Double Jump
    private bool canDoubleJump;
    private float doubleJumpPower = 20f;

    // Power Up


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D rbLowGrav;
    [SerializeField] private Rigidbody2D rbHighSpeed;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private Transform groundCheckTop;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    
    void Update()
    {

        if (isDashing)
        {
            return;
        }

        horizontal = Convert.ToSingle(Input.GetKey(KeyCode.D)) - Convert.ToSingle(Input.GetKey(KeyCode.A));

        if (isGrounded() && !Input.GetKey(KeyCode.W))
        {
            canDoubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && (isGrounded() || canDoubleJump))
        {
            rb.velocity = new Vector2(rb.velocity.x, canDoubleJump ? doubleJumpPower : jumpPower);

            canDoubleJump = !canDoubleJump;
        }

        if (Input.GetKeyDown(KeyCode.W) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            StartCoroutine(dash());
        }

        flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        
        return Physics2D.OverlapCircle(groundCheckLeft.position, 0.2f, groundLayer) ||
            Physics2D.OverlapCircle(groundCheckRight.position, 0.2f, groundLayer) ||
            Physics2D.OverlapCircle(groundCheckTop.position, 0.2f, groundLayer);
    }

    private void flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            transform.localScale = localScale;
        }
    }

    private IEnumerator dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if (isFacingRight)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
        }
        
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public static void ResetSpeed()
    {
        speed = 8f;
    }  
}
