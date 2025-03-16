using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    [Header("Assign the *child* panel, not the parent")]
    public GameObject gameOverMenuPanel;

    [Header("Optional: if you want fade/interaction control")]
    public CanvasGroup gameOverCanvasGroup;

    [Header("Reference your Player object here")]
    public GameObject player;

    [Header("Level Manager for respawning")]
    public LevelManager levelManager;

    private void Awake()
    {
        if (player != null)
        {
            // Ensure the player's GameObject is active
            player.SetActive(true);
            EnablePlayerControls();
        }
    }

    private void Start()
    {
        // Ensure the Game Over menu is hidden at the start
        if (gameOverMenuPanel != null)
        {
            gameOverMenuPanel.SetActive(false);
        }

        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 0;
            gameOverCanvasGroup.interactable = false;
            gameOverCanvasGroup.blocksRaycasts = false;
        }

        // Subscribe to player death event
        Health.OnDeath += ShowGameOverMenu;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        Health.OnDeath -= ShowGameOverMenu;
    }

    private void ShowGameOverMenu(PlayerMotor player)
    {
        gameOverMenuPanel.SetActive(true);
        Time.timeScale = 0f;

        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 1;
            gameOverCanvasGroup.interactable = true;
            gameOverCanvasGroup.blocksRaycasts = true;
        }

        DisablePlayerControls();
    }

    public void RespawnPlayer()
    {
        Time.timeScale = 1f;
        EnablePlayerControls();
        
        // Respawn the player at the latest checkpoint
        levelManager.RevivePlayer();

        gameOverMenuPanel.SetActive(false);

        if (gameOverCanvasGroup != null)
        {
            gameOverCanvasGroup.alpha = 0;
            gameOverCanvasGroup.interactable = false;
            gameOverCanvasGroup.blocksRaycasts = false;
        }
    }

    public void RestartGame()
    {
        // Reset state to avoid leaving the player disabled
        Time.timeScale = 1f;
        EnablePlayerControls();

        // Respawns
        levelManager.RevivePlayer();
        gameOverMenuPanel.SetActive(false); // Fixed from pauseMenuPanel to gameOverMenuPanel
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        EnablePlayerControls();
        SceneManager.LoadScene("Main Menu"); // Change to the correct scene name
    }

    private void DisablePlayerControls()
    {
        if (player != null)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.enabled = false;
            }
        }
    }

    private void EnablePlayerControls()
    {
        if (player != null)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }
}
