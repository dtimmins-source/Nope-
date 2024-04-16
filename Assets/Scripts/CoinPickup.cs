using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSfx;
    [SerializeField] int pointsForCoinPickup = 100;
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }    
    }

}
