using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private Animator animator;
    private bool canAct = true;
    public int health = 30;

    public Transform PointA;
    public Transform PointB;
    private bool goingToPointB = true;

    public Transform startShoot;
    public Transform endShoot;
    public GameObject Bullet;
    public GameObject Gates;

    void Start()
    {
        animator = GetComponent<Animator>();

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
}
