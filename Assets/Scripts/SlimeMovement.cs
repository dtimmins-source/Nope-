using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D slimeRigidbody;
    BoxCollider2D turnDetector;

    void Awake()
    {
        slimeRigidbody = GetComponent<Rigidbody2D>();
        turnDetector = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        slimeRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
         transform.localScale = new Vector2(-slimeRigidbody.velocity.x, 1f);
    }
}