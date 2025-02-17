using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Action Follow Target", fileName = "ActionFollowTarget")]
public class ActionFollowTarget : AIAction
{
    public float followSpeed = 3f;
    public float minDistanceToTarget = 0.1f;

    public override void Act(StateController controller)
    {
        FollowTarget(controller);
    }

    // Moves the object towards our Target
    private void FollowTarget(StateController controller)
    {
        if (controller.Target == null)
        {
            return;
        }

        Vector3 dirToTarget = controller.Target.transform.position - controller.transform.position;
        if (dirToTarget.magnitude > minDistanceToTarget)
        {
            controller.transform.Translate(dirToTarget * followSpeed * Time.deltaTime);
        }
    }
}
