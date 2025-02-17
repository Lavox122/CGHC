using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGun : Collectable
{
    [Header("Settings")]
    [SerializeField] private Gun gunPrefab;

    protected override void Collect()
    {
        EquippGun();
    }

    // Equipp the gun prefab
    private void EquippGun()
    {
        if (_playerMotor.GetComponent<GunController>() != null)
        {
            _playerMotor.GetComponent<GunController>().EquippGun(gunPrefab);
        }
    }
}
