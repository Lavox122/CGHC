using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decision Line Detection", fileName = "DecisionLineDetection")]
public class DecisionLineDetection : AIDecision
{
    public LayerMask playerMask;
    public float rayLenght = 5f;

    public override bool Decide(StateController controller)
    {
        return DetectPlayer(controller);
    }

    // Returns if the object detected the player 
    private bool DetectPlayer(StateController controller)
    {
        Vector3 dir = controller.Path.Direction == PathFollow.MoveDirections.RIGHT ? Vector3.right : Vector3.left;
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, dir, rayLenght, playerMask);
        controller.DebugRay(rayLenght, controller.transform.position, dir, hit);

        if (hit)
        {
            controller.Target = hit.collider.GetComponent<PlayerMotor>();
            return true;
        }

        controller.Target = null;
        return false;
    }
}
