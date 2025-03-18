using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossSpiralShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public Transform firepoint; // Empty GameObject as shooting point
    public float bulletSpeed = 5f; // Speed of the bullet
    public float fireRate = 0.1f; // Time between shots
    public float spiralSpeed = 10f; // Speed of spiral rotation

    private float angle = 0f; // Angle for spiral movement
    private float nextFireTime = 0f; // Timer for firing bullets

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootSpiral();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootSpiral()
    {
        if (firepoint == null || bulletPrefab == null) return;

        // Calculate direction based on the spiral angle
        float radianAngle = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

        // Create bullet and set position & rotation
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed; // Apply velocity to bullet
        }

        // Rotate the angle for next shot
        angle += spiralSpeed;
        if (angle >= 360f) angle -= 360f;
    }
}

