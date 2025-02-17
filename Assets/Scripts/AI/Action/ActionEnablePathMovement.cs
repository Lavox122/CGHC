using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Action Enable Path Movement", fileName = "ActionEnablePathMovement")]
public class ActionEnablePathMovement : AIAction
{
    public override void Act(StateController controller)
    {
        EnablePath(controller);
    }

    // Enable the path component attached to the object
    private void EnablePath(StateController controller)
    {
        if (controller.Target == null)
        {
            controller.Path.enabled = true;
        }
    }
}
