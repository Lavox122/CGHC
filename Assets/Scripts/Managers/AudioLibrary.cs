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

    // The Properties for our clips!
    public AudioClip JumpClip => jumpClip;
    public AudioClip CollectableClip => collectableClip;
    public AudioClip ProjectileClip => projectileClip;
    public AudioClip EnemyProjectileClip => enemyProjectileClip;
    public AudioClip PlayerDeadClip => playerDeadClip;
    public AudioClip ProjectileCollisionClip => projectileCollisionClip;
}
