using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action<int> OnLifesChanged;
    public static Action<PlayerMotor> OnDeath;
    public static Action<PlayerMotor> OnRevive;

    [Header("Settings")]
    [SerializeField] private int lifes = 3;

    public int MaxLifes => _maxLifes;

    public int CurrentLifes => _currentLifes;

    private int _maxLifes;
    private int _currentLifes;

    private void Awake()
    {
        _maxLifes = lifes;
    }

    private void Start()
    {
        ResetLife();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            LoseLife();
        }
    }

    public void AddLife()
    {
        _currentLifes += 1;
        if (_currentLifes > _maxLifes)
        {
            _currentLifes = _maxLifes;
        }

        UpdateLifesUI();
    }

    public void LoseLife()
    {
        _currentLifes -= 1;
        if (_currentLifes <= 0)
        {
            _currentLifes = 0;
            OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerDeadClip);
        }

        UpdateLifesUI();
    }

    public void KillPlayer()
    {
        _currentLifes = 0;
        SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerDeadClip);
        UpdateLifesUI();
        OnDeath?.Invoke(gameObject.GetComponent<PlayerMotor>());
    }

    public void ResetLife()
    {
        _currentLifes = lifes;
        UpdateLifesUI();
    }

    public void Revive()
    {
        OnRevive?.Invoke(gameObject.GetComponent<PlayerMotor>());
    }

    private void UpdateLifesUI()
    {
        OnLifesChanged?.Invoke(_currentLifes);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().Damage(gameObject.GetComponent<PlayerMotor>());
        }
    }
}
