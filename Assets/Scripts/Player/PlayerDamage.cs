using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void Collision(Collider2D objectCollided)
    {
        Health health = objectCollided.GetComponent<Health>();
        if (health != null)
        {
            // Reduce the player's life first
            health.LoseLife();
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
