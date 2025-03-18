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
            Destroy(other.gameObject); // Instantly destroy the player
            Destroy(gameObject); // Destroy the bullet
        }
    }
}


