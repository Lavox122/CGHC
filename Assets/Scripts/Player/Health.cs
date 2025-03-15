using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Action<int> OnLifesChanged;
    public static Action<PlayerMotor> OnDeath;
    public static Action<PlayerMotor> OnRevive;

    [Header("Settings")]
    [SerializeField] private int lifes = 5; // Number of lifes the player has
    
    [Header("UI Settings")]
    [SerializeField] private Image healthFillImage; // Assign UI Image in Inspector
    [SerializeField] private Material healthMaterial; // Assign Material with Alpha Mask shader
    
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

        if(Input.GetKeyDown(KeyCode.K))
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

        // Assuming you have a method to update the health bar visuals
        UpdateHealthBarVisuals(_currentLifes, _maxLifes);
    }

    private void UpdateHealthBarVisuals(int currentLifes, int maxLifes)
    {
        // Implement the logic to update the health bar visuals here
        // This could involve setting the fill amount of the healthFillImage
        // or updating the material properties of the healthMaterial

        // Example: Update the fill amount of the healthFillImage
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = (float)currentLifes / maxLifes;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().Damage(gameObject.GetComponent<PlayerMotor>());
        }
    }
}