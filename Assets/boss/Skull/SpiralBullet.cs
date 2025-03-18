using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f); // Destroy the bullet after 5 seconds
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.LoseLife(); // Reduce player's life instead of destroying the object
            }

            Destroy(gameObject); // Destroy the bullet after hitting the player
        }
    }
}


