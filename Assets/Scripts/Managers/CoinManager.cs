using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    // Control of the coins we have
    public int TotalCoins { get; set; }

    private string COINS_KEY = "MyGame_TOTAL_COINS";

    private void Start()
    {
        // Reset coins at the start of the game
        ResetCoins();
    }

    // Resets the coins to 0 and updates PlayerPrefs
    public void ResetCoins()
    {
        TotalCoins = 0;
        PlayerPrefs.SetInt(COINS_KEY, TotalCoins);
        PlayerPrefs.Save();
        Debug.Log("Coin count reset to 0.");
    }

    // Adds coins to our total
    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        PlayerPrefs.SetInt(COINS_KEY, TotalCoins);
        PlayerPrefs.Save();
    }

    // Removes coins from our total
    public void RemoveCoins(int amount)
    {
        TotalCoins -= amount;
        PlayerPrefs.SetInt(COINS_KEY, TotalCoins);
        PlayerPrefs.Save();
    }
}
