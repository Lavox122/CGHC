using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void Collision(Collider2D objectCollided)
    {
        if (objectCollided.GetComponent<Health>() != null)
        {
            objectCollided.GetComponent<Health>().LoseLife();
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
