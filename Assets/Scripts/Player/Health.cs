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
        Debug.Log("Health initialized: " + _currentLifes); // Debug log
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
        if (_currentLifes > 0) // Prevent negative values
        {
            _currentLifes -= 1;
        }

        Debug.Log("Life Lost! Current Lives: " + _currentLifes); // Debug log

        if (_currentLifes <= 0)
        {
            _currentLifes = 0;
            OnDeath?.Invoke(GetComponent<PlayerMotor>());
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
        Debug.Log("Updating UI: Current Lives = " + _currentLifes);
        
        OnLifesChanged?.Invoke(_currentLifes); // Notify UIManager to update health UI
        UIManager.Instance.OnPlayerLifes(_currentLifes);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().Damage(gameObject.GetComponent<PlayerMotor>());
        }
    }
}