using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerJetpack : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jetpackForce = 10f;
    [SerializeField] private float jetpackFuel = 5f;
    [SerializeField] private float jetpackPitch = 1f;

    public float JetpackFuel { get; set; }
    public float FuelLeft { get; set; }
    public float InitialFuel => jetpackFuel;

    private float _fuelLeft;
    private float _fuelDuractionLeft;
    private bool _stillHaveFuel = true;

    private int _jetpackParameter = Animator.StringToHash("Jetpack");

    // Declare the AudioSource for the looping jetpack sound
    private AudioSource _jetpackAudioSource;

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

        // If not already jetpacking, start playing the looping jetpack sound
        if (!_playerController.Conditions.IsJetpacking)
        {
            if (_jetpackAudioSource == null)
            {
                // Create an AudioSource component for the jetpack sound
                _jetpackAudioSource = gameObject.AddComponent<AudioSource>();
                _jetpackAudioSource.clip = AudioLibrary.Instance.JetpackClip;
                _jetpackAudioSource.loop = true;
                _jetpackAudioSource.pitch = jetpackPitch;
            }
            if (!_jetpackAudioSource.isPlaying)
            {
                _jetpackAudioSource.Play();
            }
        }

        _playerController.SetVerticalForce(jetpackForce);
        _playerController.Conditions.IsJetpacking = true;
        StartCoroutine(BurnFuel());
    }

    private void EndJetpack()
    {
        _playerController.Conditions.IsJetpacking = false;

        // Immediately stop and remove the looping jetpack audio if it's playing,
        // ensuring an instant cutoff of the sound.
        if (_jetpackAudioSource != null)
        {
            _jetpackAudioSource.Stop();
            Destroy(_jetpackAudioSource);
            _jetpackAudioSource = null;
        }

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
        yield return new WaitForSeconds(1.5f);
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
