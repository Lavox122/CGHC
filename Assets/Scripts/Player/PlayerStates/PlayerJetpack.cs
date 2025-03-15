using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerJetpack : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jetpackForce = 10f;
    [SerializeField] private float jetpackFuel = 5f;

    public float JetpackFuel { get; set; }
    public float FuelLeft { get; set; }
    public float InitialFuel => jetpackFuel;

    private float _fuelLeft;
    private float _fuelDuractionLeft;
    private bool _stillHaveFuel = true;

    private int _jetpackParameter = Animator.StringToHash("Jetpack");

    protected override void InitState()
    {
        base.InitState();
        JetpackFuel = jetpackFuel;
        _fuelDuractionLeft = JetpackFuel;
        FuelLeft = JetpackFuel;
        UIManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);
    }

    protected override void GetInput()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Jetpack();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            EndJetpack();
        }
    }

    private void Jetpack()
    {
        if (!_stillHaveFuel)
        {
            return;
        }

        if (FuelLeft <= 0)
        {
            EndJetpack();
            _stillHaveFuel = false;
            return;
        }

        _playerController.SetVerticalForce(jetpackForce);
        _playerController.Conditions.IsJetpacking = true;
        StartCoroutine(BurnFuel());
    }

    private void EndJetpack()
    {
        _playerController.Conditions.IsJetpacking = false;
        StartCoroutine(Refill());
    }

    private IEnumerator BurnFuel()
    {
        float fuelConsumed = FuelLeft;
        if (fuelConsumed > 0 && _playerController.Conditions.IsJetpacking && FuelLeft <= fuelConsumed)
        {
            fuelConsumed -= Time.deltaTime;
            FuelLeft = fuelConsumed;
            UIManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);
            yield return null;
        }
    }

    private IEnumerator Refill()
    {
        yield return new WaitForSeconds(0.5f);
        float fuel = FuelLeft;
        while (fuel < JetpackFuel && !_playerController.Conditions.IsJetpacking)
        {
            fuel += Time.deltaTime;
            FuelLeft = fuel;
            UIManager.Instance.UpdateFuel(FuelLeft, JetpackFuel);

            if (!_stillHaveFuel && fuel > 0.2f)
            {
                _stillHaveFuel = true;
            }

            yield return null;
        }
    }

    public override void SetAnimation()
    {
        _animator.SetBool(_jetpackParameter, _playerController.Conditions.IsJetpacking);
    }
}
