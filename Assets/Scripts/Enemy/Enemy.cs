using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 10; // Score given when enemy is killed

    private void Collision(Collider2D objectCollided)
    {
        if (objectCollided.GetComponent<StateController>() != null)
        {
            // Add score when the enemy is destroyed
            CoinManager.Instance.AddCoins(scoreValue);

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
