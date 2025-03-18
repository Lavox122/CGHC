using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LastLevelVictoryScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private GameObject victoryPanel;

    private void Start()
    {
        // Ensure victory screen is hidden at the start
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // Assign button function
        if (MainMenuButton != null)
        {
            MainMenuButton.onClick.AddListener(LoadMainMenu);
        }
    }

    public void ShowVictoryScreen()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        UpdateFinalScore();
        Time.timeScale = 0f; // Pause the game
    }

    private void UpdateFinalScore()
    {
        int totalCoins = CoinManager.Instance.TotalCoins;
        finalScoreText.text = "Final Score: " + totalCoins.ToString("D8");
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
