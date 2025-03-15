using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Settings")]
    [SerializeField] private Image fuelImage;
    [SerializeField] private GameObject[] playerLifes;

    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI coinTMP;

    private float _currentJetpackFuel;
    private float _jetpackFuel;

    private void Update()
    {
        InternalJetpackUpdate();
        UpdateCoins();
    }

    // Gets the fuel values
    public void UpdateFuel(float currentFuel, float maxFuel)
    {
        _currentJetpackFuel = currentFuel;
        _jetpackFuel = maxFuel;
    }

    // Updates the jetpack fuel
    private void InternalJetpackUpdate()
    {
        fuelImage.fillAmount =
            Mathf.Lerp(fuelImage.fillAmount, _currentJetpackFuel / _jetpackFuel, Time.deltaTime * 10f);
    }

    // Updates the coins
    private void UpdateCoins()
    {
        coinTMP.text = CoinManager.Instance.TotalCoins.ToString();
    }

    private int lastLivesCount = -1; // Store the last known lives count

    public void OnPlayerLifes(int currentLifes)
    {
        if (currentLifes == lastLivesCount) return; // Skip redundant updates
        lastLivesCount = currentLifes;

        Debug.Log("Health UI Updated: Enabling HealthBar_" + currentLifes);

        for (int i = 0; i < playerLifes.Length; i++)
        {
            playerLifes[i].SetActive(i < currentLifes);
        }
    }

    private void OnEnable()
    {
        Health.OnLifesChanged += OnPlayerLifes;

        // Ensure the health UI updates when the game starts
        if (FindObjectOfType<Health>() != null)
        {
            OnPlayerLifes(FindObjectOfType<Health>().CurrentLifes);
        }
    }

    private void OnDisable()
    {
        Health.OnLifesChanged -= OnPlayerLifes;
    }
}