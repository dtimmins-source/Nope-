using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    CapsuleCollider2D playerCapsuleCollider;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        LayerMask climbingLayerMask = LayerMask.GetMask("Climbing");
        if(!playerCapsuleCollider.IsTouchingLayers(climbingLayerMask))
        {
            return;
        }
        Vector2 playerClimbingVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
        playerRigidbody.velocity = playerClimbingVelocity;
    }

    // Player input messages.
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        LayerMask groundLayerMask = LayerMask.GetMask("Ground");
        if(!playerCapsuleCollider.IsTouchingLayers(groundLayerMask))
        {
            return;
        }
        if (value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
}
