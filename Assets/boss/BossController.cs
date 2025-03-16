using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    public int maxHealth = 100;
    private int currentHealth;
    
    private int attackStage = 1;
    private int increasingBulletCount = 1;
    
    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(AttackPattern());
    }

    void Update()
    {
        UpdateAttackStage();
    }

    void UpdateAttackStage()
    {
        if (currentHealth <= maxHealth * 0.25f) 
            attackStage = 3; // Stage 3: Increasing bullets
        else if (currentHealth <= maxHealth * 0.5f) 
            attackStage = 2; // Stage 2: Spiral pattern
    }

    IEnumerator AttackPattern()
    {
        while (currentHealth > 0)
        {
            if (attackStage == 1)
            {
                BlastPattern();
            }
            else if (attackStage == 2)
            {
                SpiralPattern();
            }
            else if (attackStage == 3)
            {
                IncreasingBulletPattern();
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    void BlastPattern()
    {
        int bulletCount = 5;
        float angleStep = 30f;
        float startAngle = -((bulletCount - 1) / 2f) * angleStep;
        
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (i * angleStep);
            ShootBullet(angle);
        }
    }

    void SpiralPattern()
    {
        float angleStep = 10f;
        for (int i = 0; i < 36; i++) // 360 degrees / 10 = 36 shots
        {
            float angle = i * angleStep;
            ShootBullet(angle);
        }
    }

    void IncreasingBulletPattern()
    {
        for (int i = 0; i < increasingBulletCount; i++)
        {
            ShootBullet(Random.Range(0f, 360f)); // Shoot in random directions
        }
        increasingBulletCount++; // Increase bullets for the next shot
    }

    void ShootBullet(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(bulletPrefab, firePoint.position, rotation);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
