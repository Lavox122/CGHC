using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public CanvasGroup pauseMenuCanvasGroup;
    public GameObject player;

    void Start()
    {
        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 0;
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }

        if (player != null)
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Time.timeScale = 0f;

        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 1;
            pauseMenuCanvasGroup.interactable = true;
            pauseMenuCanvasGroup.blocksRaycasts = true;
        }

        DisablePlayerControls();
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Input.ResetInputAxes();

        if (pauseMenuCanvasGroup != null)
        {
            pauseMenuCanvasGroup.alpha = 0;
            pauseMenuCanvasGroup.interactable = false;
            pauseMenuCanvasGroup.blocksRaycasts = false;
        }

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
}
