using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private Animator animator;
    private bool canAct = true;
    public int health = 30;

    // Start is called before the first frame update
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

            int action = Random.Range(0, 3);
            if (action == 0)
            {
                animator.SetTrigger("Jump");
            }
            else
            {
                animator.SetTrigger("Attack");
            }

            yield return new WaitForSeconds(GetAnimationLength());

            yield return new WaitForSeconds(4f);
            canAct = true;
        }
    }

    private float GetAnimationLength()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }

    private void TakeDamage(Collider2D objectCollided)
    {
        if (objectCollided.GetComponent<StateController>() != null)
        {
            Destroy(objectCollided.gameObject);
            health -= 1;

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 40f);
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
