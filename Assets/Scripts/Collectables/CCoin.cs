using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoin : Collectable
{
    [Header("Settings")]
    [SerializeField] private int amountToAdd = 10;

    protected override void Collect()
    {
        SoundManager.Instance.PlaySound(AudioLibrary.Instance.CollectableClip);
        AddCoin();
    }

    // Adds coins to our Global counter
    private void AddCoin()
    {
        CoinManager.Instance.AddCoins(amountToAdd);
    }
}
