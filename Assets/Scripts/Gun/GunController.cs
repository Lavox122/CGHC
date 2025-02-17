using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Gun gun;
    [SerializeField] private Transform holder;

    // Reference of the Player owner of the Gun
    public PlayerController PlayerController { get; set; }

    private Gun _gunEquipped;
    private int gunDirection;

    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        //EquippGun(gun);    // Because we want the Player to pick up the gun as item collectable, not equip directly
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Reload();
        }
    }

    // Reloads this Gun
    private void Reload()
    {
        if (_gunEquipped != null)
        {
            _gunEquipped.Reload(false);
        }
    }

    // Shoots Projectiles
    private void Shoot()
    {
        _gunEquipped.Shoot();
    }

    // Equipp a Gun
    public void EquippGun(Gun newGun)
    {
        if (_gunEquipped == null)
        {
            _gunEquipped = Instantiate(newGun, holder.position, Quaternion.identity);
            _gunEquipped.GunController = this;  // Player equips the gun

            if (PlayerController.FacingRight)
            {
                gunDirection = 1;
            }
            else
            {
                gunDirection = -1;
            }
            _gunEquipped.transform.localScale = new Vector3(gunDirection, 1, 1);

            _gunEquipped.transform.SetParent(holder);
        }
    }
}
