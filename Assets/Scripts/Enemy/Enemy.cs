using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Destroy this enemy if it is colliding with the player's projectile
    private void Collision(Collider2D objectCollided)
    {
        if (objectCollided.GetComponent<StateController>() != null)
        {
            Destroy(objectCollided.gameObject);
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
