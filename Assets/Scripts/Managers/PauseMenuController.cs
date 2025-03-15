using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false; // Global pause flag
    public GameObject pauseMenuUI; // Assign in Inspector
    public CanvasGroup pauseMenuCanvasGroup; // Assign in Inspector
    public GameObject player; // Assign Player GameObject in Inspector

    void Start()
    {
        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 0; // Hide menu initially
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Press ESC to pause/unpause
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0.01f; // Almost stopped, but UI still responsive

        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 1; // Show menu
            pauseMenuCanvasGroup.interactable = true;
            pauseMenuCanvasGroup.blocksRaycasts = true;
        }

        DisablePlayerControls();
        Debug.Log("Game is PAUSED.");
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume game time

        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 0; // Hide menu
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }

        EnablePlayerControls();
        Debug.Log("Game is RESUMED.");
    }

    private void DisablePlayerControls()
    {
        if (player != null)
        {
            player.GetComponent<PlayerController>().enabled = false; // Disable movement
        }
    }

    private void EnablePlayerControls()
    {
        if (player != null)
        {
            player.GetComponent<PlayerController>().enabled = true; // Re-enable movement
        }
    }
}
