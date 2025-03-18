using UnityEngine;

public class StageCleared : MonoBehaviour
{
    public VictoryMenuController victoryMenu; // Used for normal levels
    public LastVictoryController lastVictoryMenu; // Used for the last level

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the end checkpoint!");

            // Play the stage clear sound
            SoundManager.Instance.PlaySound(AudioLibrary.Instance.StageClearClip);

            // Trigger the appropriate victory menu
            if (lastVictoryMenu != null) 
            {
                lastVictoryMenu.ShowVictoryMenu(); // Last level
            }
            else if (victoryMenu != null) 
            {
                victoryMenu.ShowVictoryMenu(); // Other levels
            }
        }
    }
}
