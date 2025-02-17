using System;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Action Shoot Target", fileName = "ActionShootTarget")]
public class ActionShootTarget : AIAction
{
    public float msBetweenShots = 100f;
    private float nextShotTime;

    public override void Act(StateController controller)
    {
        FireTarget(controller);
    }

    // Fire projectiles towards the target position
    private void FireTarget(StateController controller)
    {
        if (controller.Pooler == null || controller.Target == null)
        {
            return;
        }

        if (Time.time > nextShotTime)
        {
            // Get Direction to target
            Vector3 dirToTarget = controller.Target.transform.position - controller.transform.position;
            Vector3 normalizeDirToTarget = dirToTarget.normalized;

            // Get Projectile from pool
            GameObject ghostProjectile = controller.Pooler.GetObjectFromPool();
            ghostProjectile.transform.position = controller.FirePoint.position;
            ghostProjectile.SetActive(true);

            // Get projectile reference
            Projectile newProjectile = ghostProjectile.GetComponent<Projectile>();
            newProjectile.SetDirection(new Vector3(normalizeDirToTarget.x, normalizeDirToTarget.y, 0f));
            newProjectile.EnableProjectile();

            SoundManager.Instance.PlaySound(AudioLibrary.Instance.EnemyProjectileClip);

            // Update shot time
            nextShotTime = Time.time + msBetweenShots / 100f;   // We purposely set the fire-rate low
        }
    }

    private void OnEnable()
    {
        nextShotTime = 0f;
    }
}
