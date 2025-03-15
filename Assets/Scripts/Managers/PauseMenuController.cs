using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed!");  // Debug log to check if input is detected
            
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");  // Debug log to check if PauseGame() is called
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // Freezes the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");  // Debug log to check if ResumeGame() is called
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  // Unfreezes the game
        isPaused = false;
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");  // Debug log for restart
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu...");  // Debug log for main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Make sure this scene is in Build Settings
    }
}
