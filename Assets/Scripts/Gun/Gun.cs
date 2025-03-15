using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator modelAnimator;

    [Header("Gun Settings")]
    [SerializeField] private float msBetweenShots = 100;

    [Header("Ammo")]
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true;
    [SerializeField] private float reloadTime = 3f;

    [Header("Visuals")]
    [SerializeField] private Sprite normalGunSprite; // Original gun sprite
    [SerializeField] private Sprite emptyGunSprite; // Gun sprite when out of ammo
    [SerializeField] private GameObject magazinePrefab; // Magazine object to drop
    [SerializeField] private Transform magazineDropPoint; // Where the magazine appears

    // Reference to GunController
    public GunController GunController { get; set; }

    private ObjectPooler _pooler;
    private SpriteRenderer _spriteRenderer; // Automatically gets the gun's SpriteRenderer
    private float _nextShotTime;
    private bool _isReloading;
    private int _projectilesRemaining;

    private int _fireParameter = Animator.StringToHash("Fire");

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer from the same object
        _projectilesRemaining = magazineSize;

        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = normalGunSprite; // Set initial sprite
        }
        else
        {
            Debug.LogError("Gun is missing a SpriteRenderer!", this);
        }
    }

    private void Update()
    {
        if (autoReload)
        {
            Reload(true);
        }
    }

    // Fires a projectile from the firePoint
    private void FireProjectile()
    {
        GameObject newProjectile = _pooler.GetObjectFromPool();
        newProjectile.transform.position = firePoint.position;
        newProjectile.SetActive(true);

        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.GunEquipped = this;
        projectile.SetDirection(GunController.PlayerController.FacingRight ? Vector3.right : Vector3.left);
        projectile.EnableProjectile();

        modelAnimator.SetTrigger(_fireParameter);
    }

    // Shoots the Gun
    public void Shoot()
    {
        if (Time.time > _nextShotTime && !_isReloading && _projectilesRemaining > 0)
        {
            _nextShotTime = Time.time + msBetweenShots / 1000f;
            FireProjectile();
            _projectilesRemaining--;

            SoundManager.Instance.PlaySound(AudioLibrary.Instance.ProjectileClip);

            if (_projectilesRemaining == 0 && _spriteRenderer != null)
            {
                _spriteRenderer.sprite = emptyGunSprite; // Change gun sprite when empty
            }
        }
    }

    // Reloads the gun
    public void Reload(bool autoReload)
    {
        if (_projectilesRemaining > 0 && _projectilesRemaining <= magazineSize && !_isReloading && !autoReload)
        {
            StartCoroutine(IEWaitForReload());
        }

        if (_projectilesRemaining <= 0 && !_isReloading)
        {
            StartCoroutine(IEWaitForReload());
        }
    }

    // Reload coroutine
    private IEnumerator IEWaitForReload()
    {
        _isReloading = true;

        // Drop a magazine object
        GameObject droppedMag = Instantiate(magazinePrefab, magazineDropPoint.position, Quaternion.identity);
        Destroy(droppedMag, 2f); // Destroy the magazine after 2 seconds

        yield return new WaitForSeconds(reloadTime);

        _projectilesRemaining = magazineSize;
        _isReloading = false;

        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = normalGunSprite; // Restore gun sprite after reload
        }
    }
}
