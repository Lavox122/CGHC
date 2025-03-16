using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkullBossAI : MonoBehaviour
{
    private Animator animator;
    private bool canAct = true;
    public int health = 100;
    private int maxHealth = 100;

    public Transform shootPoint;
    public GameObject bulletPrefab;
    public GameObject BossHP;
    public Slider healthBar;
    public Transform player;

    public float fireRate = 1f;
    private float nextFireTime = 0f;
    private float spiralAngle = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        BossHP.SetActive(true);

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }

        StartCoroutine(BossActionLoop());
    }

    IEnumerator BossActionLoop()
    {
        while (health > 0)
        {
            yield return new WaitUntil(() => canAct);
            canAct = false;

            if (health > maxHealth / 2)
            {
                ShootAtPlayer();
            }
            else
            {
                StartCoroutine(SpiralPattern());
            }

            yield return new WaitForSeconds(1f);
            canAct = true;
        }
    }

    private void ShootAtPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - shootPoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 5f;
    }

    IEnumerator SpiralPattern()
    {
        int shots = 12; // Number of bullets per spiral wave
        for (int i = 0; i < shots; i++)
        {
            float angle = spiralAngle + (360f / shots) * i;
            Vector2 shootDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 5f;
        }

        spiralAngle += 10f; // Rotate the spiral over time
        yield return new WaitForSeconds(0.3f);
    }

    private void TakeDamage(Collider2D objectCollided)
    {
        if (objectCollided.CompareTag("PlayerProjectile"))
        {
            health -= 1;
            Debug.Log("Skull Boss Health: " + health);

            if (healthBar != null)
            {
                healthBar.value = health;
            }

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        BossHP.SetActive(false);
        animator.SetTrigger("Die");
    }

    private void OnEnable()
    {
        ProjectilePooler.OnProjectileCollision += TakeDamage;
    }

    private void OnDisable()
    {
        ProjectilePooler.OnProjectileCollision -= TakeDamage;
    }
}
