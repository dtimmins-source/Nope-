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
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        gravityScaleAtStart = playerRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder(); 
        Die(); 
    }

    private void Die()
    {
        LayerMask deathLayerMask = LayerMask.GetMask("Enemies", "Hazard");
        if(playerBodyCollider.IsTouchingLayers(deathLayerMask))
        {
            isAlive = false;    
            playerAnimator.SetTrigger("Dying");
            playerRigidbody.velocity = deathKick;
            playerBodyCollider.enabled = false;
            playerFeetCollider.enabled = false;
        }
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
        if(!playerFeetCollider.IsTouchingLayers(climbingLayerMask))
        {
            playerAnimator.SetBool("isClimbing", false);
            playerRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        playerRigidbody.gravityScale = 0f;
        Vector2 playerClimbingVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
        playerRigidbody.velocity = playerClimbingVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    // Player input messages.
    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) {return;}
        LayerMask groundLayerMask = LayerMask.GetMask("Ground");
        if(!playerFeetCollider.IsTouchingLayers(groundLayerMask))
        {
            return;
        }
        if (value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
}
