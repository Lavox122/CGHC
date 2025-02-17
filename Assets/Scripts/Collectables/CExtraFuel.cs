using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CExtraFuel : Collectable
{
    [Header("Settings")]
    [SerializeField] private float extraFuel = 10f;
    [SerializeField] private float extraFuelTimer = 3f;

    private PlayerJetpack _jetpack;

    protected override void Collect()
    {
        ApplyFuel();
    }

    // Apply that bonus
    private void ApplyFuel()
    {
        _jetpack = _playerMotor.GetComponent<PlayerJetpack>();
        StartCoroutine(IEExtraFuel());
    }

    // Adds fuel 
    private IEnumerator IEExtraFuel()
    {
        _jetpack.JetpackFuel = extraFuel;
        _jetpack.FuelLeft = extraFuel;
        UIManager.Instance.UpdateFuel(extraFuel, extraFuel);
        yield return new WaitForSeconds(extraFuelTimer);
        _jetpack.JetpackFuel = _jetpack.InitialFuel;
        _jetpack.FuelLeft = _jetpack.InitialFuel;
        UIManager.Instance.UpdateFuel(_jetpack.FuelLeft, _jetpack.JetpackFuel);
    }
}
