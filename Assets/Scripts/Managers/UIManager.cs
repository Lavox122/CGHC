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

    // Updates the player lifes
    private void OnPlayerLifes(int currentLifes)
    {
        for (int i = 0; i < playerLifes.Length; i++)
        {
            if (i < currentLifes) // Total is 3, so value = 2
            {
                playerLifes[i].gameObject.SetActive(true);
            }
            else
            {
                playerLifes[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        Health.OnLifesChanged += OnPlayerLifes;
    }

    private void OnDisable()
    {
        Health.OnLifesChanged -= OnPlayerLifes;
    }
}
