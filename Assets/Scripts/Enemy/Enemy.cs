using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 10; // Score given when enemy is killed

    private void Collision(Collider2D objectCollided)
    {
        if (objectCollided.GetComponent<StateController>() != null && objectCollided.gameObject == gameObject)
        {
            // Add score when the enemy is destroyed
            CoinManager.Instance.AddCoins(scoreValue);

            // Play the enemy dead sound sound effect
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.EnemyDeathClip);

            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        ProjectilePooler.OnProjectileCollision += Collision;
    }

    private void OnDisable()
    {
        ProjectilePooler.OnProjectileCollision -= Collision;
    }
}

