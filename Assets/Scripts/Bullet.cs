using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    Rigidbody2D bulletRigidBody;
    PlayerMovement player;
    float xSpeed;

    void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>();    
        player = FindObjectOfType<PlayerMovement>();  
        xSpeed = player.transform.localScale.x * bulletSpeed;  
    }

    void Update()
    {
        bulletRigidBody.velocity = new Vector2(xSpeed, 0f);
        transform.localScale = new Vector2(Mathf.Sign(xSpeed), 1f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);         
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
