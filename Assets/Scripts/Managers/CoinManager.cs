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
        LoadCoins();
    }

    //private void Update()       //This is for SIMULATION purposes, we can delete the UPDATE method later
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        AddCoins(10);
    //    }
    //}

    // Load the coins saved
    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(COINS_KEY, 0);
    }

    // Adds coins to our Global
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
