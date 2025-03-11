using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private Animator animator;
    private bool canAct = true;
    public int health = 30;

    public Transform PointA;
    public Transform PointB;
    private bool moving;

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

            int action = Random.Range(0, 2);
            if (action == 0)
            {
                animator.SetTrigger("Jump");

                yield return new WaitForSeconds(GetAnimationLength("Jump"));

                Teleport();
            }
            else
            {
                animator.SetTrigger("Attack");
            }

            yield return new WaitForSeconds(4f);
            canAct = true;
        }
    }

    private float GetAnimationLength(string animationName)
    {
        float timeout = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < timeout)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            while (!stateInfo.IsName(animationName))
            {
                return stateInfo.length;
            }
            elapsedTime += Time.deltaTime;
        }

        return 1f;
        
    }

    private void Teleport()
    {
        moving = !moving;
        Transform target = moving ? PointA : PointB;
        transform.position = target.position;
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
