using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuController : MonoBehaviour
{
    [Header("Assign the *child* panel, not the parent")]
    public GameObject victoryMenuPanel;

    [Header("Optional: if you want fade/interaction control")]
    public CanvasGroup victoryMenuCanvasGroup;

    [Header("Reference your Player object here")]
    public GameObject player;

    [Header("Next Level Scene Name")]
    public string nextLevelName = "NextLevelScene"; // Set the actual name of the next level

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
        // Ensure the game is running normally
        Time.timeScale = 1f;

        // Hide the child panel
        if (victoryMenuPanel != null)
        {
            victoryMenuPanel.SetActive(false);
        }

        // Reset CanvasGroup if used
        if (victoryMenuCanvasGroup != null)
        {
            victoryMenuCanvasGroup.alpha = 0;
            victoryMenuCanvasGroup.interactable = false;
            victoryMenuCanvasGroup.blocksRaycasts = false;
        }
    }

    public void ShowVictoryMenu()
    {
        // Show the child panel
        if (victoryMenuPanel != null)
        {
            victoryMenuPanel.SetActive(true);
        }

        // Stop time
        Time.timeScale = 0f;

        // Show CanvasGroup if used
        if (victoryMenuCanvasGroup != null)
        {
            victoryMenuCanvasGroup.alpha = 1;
            victoryMenuCanvasGroup.interactable = true;
            victoryMenuCanvasGroup.blocksRaycasts = true;
        }

        // Disable player controls
        DisablePlayerControls();
    }

    public void ContinueToNextLevel()
    {
        // Resume time and re-enable player controls
        Time.timeScale = 1f;
        EnablePlayerControls();

        // Load the next level
        SceneManager.LoadScene(nextLevelName);
    }

    public void LoadMainMenu()
    {
        // Reset state before switching scenes
        Time.timeScale = 1f;
        EnablePlayerControls();

        // Replace "Main Menu" with the correct scene name if needed
        SceneManager.LoadScene("Main Menu");
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
