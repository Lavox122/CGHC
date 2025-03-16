using UnityEngine;

public class StageCleared : MonoBehaviour
{
    public VictoryMenuController victoryMenu; // Assign VictoryMenuController in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the end checkpoint!");
            
            // Trigger the Victory Menu
            if (victoryMenu != null)
            {
                victoryMenu.ShowVictoryMenu();
            }
        }
    }
}
