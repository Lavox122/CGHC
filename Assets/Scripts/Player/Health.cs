using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Action<int> OnLifesChanged;
    public static Action<PlayerMotor> OnDeath;
    public static Action<PlayerMotor> OnRevive;

    [Header("Settings")]
    [SerializeField] private int lifes = 5; // Number of lifes the player has

    public int MaxLifes => _maxLifes;
    public int CurrentLifes => _currentLifes;

    private int _maxLifes;
    private int _currentLifes;

    private void Awake()
    {
        _maxLifes = lifes;
        _currentLifes = lifes; // Ensure current health is set properly
    }

    private void Start()
    {
        ResetLife();
        UpdateLifesUI(); // Ensure UI updates at start
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            LoseLife();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            AddLife();
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
        if (_currentLifes > 0)
        {
            _currentLifes -= 1;
        }

        // Update UI immediately after changing health
        UpdateLifesUI();

        // After health update, play the appropriate sound
        if (_currentLifes > 0)
        {
            // Player lost a life but is still alive
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.HealthLoseClip);
        }
        else
        {
            // Player has no lives left (dead)
            _currentLifes = 0;
            OnDeath?.Invoke(GetComponent<PlayerMotor>());
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerDeadClip);
        }
    }

    public void KillPlayer()
    {
        _currentLifes = 0;
        UpdateLifesUI();
        OnDeath?.Invoke(GetComponent<PlayerMotor>());
        SoundManager.Instance.PlaySound(AudioLibrary.Instance.PlayerDeadClip);
    }

    public void ResetLife()
    {
        _currentLifes = lifes;
        UpdateLifesUI();
    }

    public void Revive()
    {
        OnRevive?.Invoke(GetComponent<PlayerMotor>());
    }

    private void UpdateLifesUI()
    {
        OnLifesChanged?.Invoke(_currentLifes); // Notify UIManager to update health UI
        UIManager.Instance.OnPlayerLifes(_currentLifes);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().Damage(GetComponent<PlayerMotor>());
        }
    }
}
