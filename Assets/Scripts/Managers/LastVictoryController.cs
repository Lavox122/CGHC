using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LastVictoryController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject victoryMenuPanel;
    public TextMeshProUGUI finalScoreText;
    public string mainMenuScene = "Main Menu";

    private void Start()
    {
        // Ensure the game runs normally when the level starts
        Time.timeScale = 1f;

        // Hide the panel at start
        if (victoryMenuPanel != null)
        {
            victoryMenuPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When any object enters, trigger the victory menu (no need for specific tags)
        ShowVictoryMenu();
    }

    public void ShowVictoryMenu()
    {
        if (victoryMenuPanel != null)
        {
            victoryMenuPanel.SetActive(true);
        }

        // Stop time when showing the menu
        Time.timeScale = 0f;

        // Update the final score display
        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + CoinManager.Instance.TotalCoins.ToString("D8");
        }
    }

    public void LoadMainMenu()
    {
        // Resume time before switching scenes
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
