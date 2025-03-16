using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 5f; // Bullet speed
    private Vector3 startPosition;
    private float maxDistance = 10f; // Maximum travel distance

    void Start()
    {
        startPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); // Move the bullet forward

        // Check if the bullet has traveled more than maxDistance
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // Destroy bullet
        }
    }
}