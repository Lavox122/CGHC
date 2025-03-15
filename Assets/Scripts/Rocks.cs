using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    private bool hasCollided = false;

    private void Start()
    {
        Invoke("DestroyRock", 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health playerHealth = collision.GetComponent<Health>();

        if (playerHealth != null) 
        {
            hasCollided = true;
            Destroy(gameObject);
            playerHealth.LoseLife();
        }
    }

    private void DestroyRock()
    {
        if (!hasCollided) 
        { 
            Destroy(gameObject);
        }
    }
}
