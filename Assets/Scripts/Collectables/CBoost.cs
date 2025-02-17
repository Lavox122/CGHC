using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoost : Collectable
{
    [Header("Settings")]
    [SerializeField] private float boostSpeed = 30f;
    [SerializeField] private float boostTime = 3f;

    private PlayerMovement _playerMovement;

    protected override void Collect()
    {
        ApplyMovement();
    }

    // Apply that movement bonus
    private void ApplyMovement()
    {
        _playerMovement = _playerMotor.GetComponent<PlayerMovement>();
        if (_playerMovement != null)
        {
            StartCoroutine(IEBoost());
        }
    }

    // Add boost to our player movement
    private IEnumerator IEBoost()
    {
        _playerMovement.Speed = boostSpeed;
        yield return new WaitForSeconds(boostTime);
        _playerMovement.Speed = _playerMovement.InitialSpeed;
    }
}
