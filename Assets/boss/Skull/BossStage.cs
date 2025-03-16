using System.Collections;
using UnityEngine;

public class BossStage : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject spiralBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public int bossHP = 200;

    public float fireRateStage1 = 1.5f; // Adjustable firing rate for Stage 1
    public float fireRateStage2 = 2f; // Adjustable firing rate for Stage 2
    public float fireRateStage3 = 1.2f; // Adjustable firing rate for Stage 3
    
    private int stage = 1;
    private int increasingShots = 5;

    void Start()
    {
        StartCoroutine(AttackPattern());
    }

    void Update()
    {
        if (bossHP <= 100 && stage == 1)
        {
            stage = 2;
        }
        else if (bossHP <= 50 && stage == 2)
        {
            stage = 3;
        }
    }

    IEnumerator AttackPattern()
    {
        while (bossHP > 0)
        {
            if (stage == 1)
            {
                ShootStraight(5);
                yield return new WaitForSeconds(fireRateStage1);
            }
            else if (stage == 2)
            {
                yield return StartCoroutine(SpiralShoot());
                yield return new WaitForSeconds(fireRateStage2);
            }
            else if (stage == 3)
            {
                ShootStraight(increasingShots);
                yield return StartCoroutine(SpiralShoot());
                increasingShots++;
                yield return new WaitForSeconds(fireRateStage3);
            }
        }
    }

    // 🔹 Shoots bullets in a straight line
    void ShootStraight(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0; // Ensure bullet doesn't fall
                rb.velocity = Vector2.left * bulletSpeed; // Adjust direction if needed
            }

            Destroy(bullet, 10f); // Auto-destroy bullet after 10 seconds
        }
    }

    IEnumerator SpiralShoot()
    {
        int spiralBulletCount = 12;
        float angleStep = 360f / spiralBulletCount;
        float angle = 0f;

        for (int i = 0; i < spiralBulletCount; i++)
        {
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            GameObject bullet = Instantiate(spiralBulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0;
                rb.velocity = bulletDirection * bulletSpeed;
            }

            Destroy(bullet, 10f);
            angle += angleStep;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 🔹 Reduce Boss HP when hit by player's bullets
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet")) // Make sure your player bullets have the tag "PlayerBullet"
        {
            bossHP -= 10; // Adjust damage value as needed
            Destroy(collision.gameObject); // Destroy player bullet on impact

            if (bossHP <= 0)
            {
                Die();
            }
        }
    }

    // 🔹 Handle Boss death
    void Die()
    {
        Debug.Log("Boss Defeated!");
        Destroy(gameObject); // Remove boss from the scene
    }
}
