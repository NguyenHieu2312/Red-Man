using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float moveX;

    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(.3f, .03f);
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private int maxJumptime = 2;
    [SerializeField] private int jumpRemaining;

    [SerializeField] private float maxSpeedFalling;
    [SerializeField] private float baseGravity = 3f;
    [SerializeField] private float fallSpeedMultiplier = 2f;

    public Rigidbody2D rb;

    void Update()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        GroundCheck();
        Falling();
    }

    public void Move(InputAction.CallbackContext context)
    {
        this.moveX = context.ReadValue<Vector2>().x;
    }


    public void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPosition.position, groundCheckSize, 0, groundLayer))
        {
            jumpRemaining = maxJumptime;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpRemaining > 0)
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpRemaining--;
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .6f);
            }
        }
    }
    public void Falling()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxSpeedFalling));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheckPosition.position, groundCheckSize);
    }


}
