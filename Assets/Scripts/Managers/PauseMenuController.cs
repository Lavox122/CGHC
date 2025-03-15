using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false;

    [Header("Assign the *child* panel, not the parent")]
    public GameObject pauseMenuPanel;

    [Header("Optional: if you want fade/interaction control")]
    public CanvasGroup pauseMenuCanvasGroup;

    [Header("Reference your Player object here")]
    public GameObject player;

    // Awake is called before Start. This ensures the player is enabled as early as possible.
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
        // Always start unpaused
        isPaused = false;

        // Run game at normal speed
        Time.timeScale = 1f;

        // Hide the child panel
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        // Reset CanvasGroup if used
        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 0;
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        // Check Escape key in Update, not FixedUpdate
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;

        // Show the child panel
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }

        // Stop time
        Time.timeScale = 0f;

        // Show CanvasGroup if used
        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 1;
            pauseMenuCanvasGroup.interactable = true;
            pauseMenuCanvasGroup.blocksRaycasts = true;
        }

        // Disable player controls
        DisablePlayerControls();
    }

    private void ResumeGame()
    {
        isPaused = false;

        // Hide the child panel
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        // Resume time
        Time.timeScale = 1f;
        Input.ResetInputAxes();

        // Hide CanvasGroup if used
        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 0;
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }

        // Enable player controls
        EnablePlayerControls();
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

    // Restart the current scene
    public void RestartGame()
    {
        // Reset state to avoid leaving the player disabled
        isPaused = false;
        Time.timeScale = 1f;
        EnablePlayerControls();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Load the Main Menu scene
    public void LoadMainMenu()
    {
        // Reset state before switching scenes
        isPaused = false;
        Time.timeScale = 1f;
        EnablePlayerControls();

        // Replace "Main Menu" with the correct scene name if needed
        SceneManager.LoadScene("Main Menu");
    }
}
