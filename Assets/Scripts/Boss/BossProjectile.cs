using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : Projectile
{
    private Vector3 targetPosition;
    private bool hasTarget = false;

    [SerializeField] private GameObject projectilePrefab;

    protected override void Update()
    {
        if (hasTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Explode();
            }
        }
        else
        {
            base.Update();
        }
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        hasTarget = true;
    }

    private void Explode()
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45f;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);

            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = newProjectile.GetComponent<Projectile>();
            projectile.SetDirection(direction);

            if (newProjectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.velocity = direction * projectile.Speed;
            }
        }

        Destroy(gameObject);
    }
}
