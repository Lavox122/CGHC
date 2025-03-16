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

    public GameObject MagazinePrefab;
    public Transform dropPoint;

    public GameObject ReloadingModel;
    public GameObject Model;

    // Reference to GunController
    public GunController GunController { get; set; }

    private ObjectPooler _pooler;
    private float _nextShotTime;
    private bool _isReloading;
    private int _projectilesRemaining;

    private int _fireParameter = Animator.StringToHash("Fire");

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _projectilesRemaining = magazineSize;
    }

    private void Update()
    {
        if (autoReload)
        {
            Reload(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(false);
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

            SoundManager.Instance.PlaySound(AudioLibrary.Instance.GunShootClip);
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
        ReloadingModel.SetActive(true);
        Model.SetActive(false);

        Instantiate(MagazinePrefab, dropPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(reloadTime);

        Model.SetActive(true);
        ReloadingModel.SetActive(false);

        _projectilesRemaining = magazineSize;
        _isReloading = false;
    }
}
