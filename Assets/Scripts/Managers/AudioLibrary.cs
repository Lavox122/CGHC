using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : Singleton<AudioLibrary>
{
    [Header("Clips")]
    [SerializeField] private AudioClip collectableClip;
    [SerializeField] private AudioClip enemyProjectileClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip playerDeadClip;
    [SerializeField] private AudioClip projectileClip;
    [SerializeField] private AudioClip projectileCollisionClip;
    [SerializeField] private AudioClip jetpackClip;
    [SerializeField] private AudioClip potionPickupClip;
    [SerializeField] private AudioClip healthLoseClip;
    [SerializeField] private AudioClip gunShootClip;
    [SerializeField] private AudioClip enemyDeadClip;
    [SerializeField] private AudioClip stageClearClip;

    // The Properties for our clips!
    public AudioClip JumpClip => jumpClip;
    public AudioClip CollectableClip => collectableClip;
    public AudioClip ProjectileClip => projectileClip;
    public AudioClip EnemyProjectileClip => enemyProjectileClip;
    public AudioClip PlayerDeadClip => playerDeadClip;
    public AudioClip ProjectileCollisionClip => projectileCollisionClip;
    public AudioClip JetpackClip => jetpackClip;
    public AudioClip PotionPickupClip => potionPickupClip;
    public AudioClip HealthLoseClip => healthLoseClip;
    public AudioClip GunShootClip => gunShootClip;
    public AudioClip EnemyDeadClip => enemyDeadClip;
    public AudioClip StageClearClip => stageClearClip;
}
