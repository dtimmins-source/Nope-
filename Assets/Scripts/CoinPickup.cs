using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSfx;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
            Destroy(gameObject);
        }    
    }

}
