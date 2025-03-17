using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    private Animator animator;
    private bool canAct = true;
    public int health = 100;
    private int maxHealth = 100;

    public Transform PointA;
    public Transform PointB;
    private bool goingToPointB = true;

    public Transform startShoot;
    public Transform endShoot;
    public GameObject Bullet;
    public GameObject Gates;
    public GameObject BossHP;

    public GameObject RocksPrefab;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    public Slider healthBar;

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

            int action = Random.Range(0, 2);
            if (action == 0)
            {
                animator.SetTrigger("Jump");
                yield return new WaitForSeconds(0.2f);
                yield return new WaitUntil(() => !canAct);
            }
            else
            {
                animator.SetTrigger("Attack");
            }

            yield return new WaitForSeconds(4f);
            canAct = true;
        }
    }

    private void Teleport()
    {
        goingToPointB = !goingToPointB;
        Transform target = goingToPointB ? PointA : PointB;
        transform.parent.position = target.position;
    }

    private void TakeDamage(Collider2D objectCollided)
    {
        if (objectCollided.GetComponent<BossAI>() != null)
        {
            health -= 1;
            Debug.Log("Boss Health:" + health);

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

        Gates.SetActive(false);
        BossHP.SetActive(false);
        animator.SetTrigger("Die");
    }

    private void RockDrop()
    {
        int numberOfRocks = Random.Range(3, 6);

        for (int i = 0; i < numberOfRocks; i++) 
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Instantiate(RocksPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 center = new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2, (spawnAreaMin.y + spawnAreaMax.y) / 2, 0);
        Vector3 size = new Vector3(spawnAreaMax.x - spawnAreaMin.x, spawnAreaMax.y - spawnAreaMin.y, 1);

        Gizmos.DrawWireCube(center, size);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        return new Vector2 (randomX, randomY);
    }

    private void OnEnable()
    {
        ProjectilePooler.OnProjectileCollision += TakeDamage;
    }

    private void OnDisable()
    {
        ProjectilePooler.OnProjectileCollision -= TakeDamage;
    }

    private void Shoot()
    {
        Vector3 fixedTargetPosition = endShoot.position;

        GameObject projectile = Instantiate(Bullet, startShoot.position, Quaternion.identity);
        projectile.GetComponent<BossProjectile>().SetTarget(fixedTargetPosition);
    }

    public void AnimationEvent_Teleport()
    {
        Teleport();
        animator.SetTrigger("JumpFinished");
    }

    public void AnimationEvent_Shoot()
    {
        Shoot();
    }

    public void AnimationEvent_Rocks()
    {
        RockDrop();
    }
}
