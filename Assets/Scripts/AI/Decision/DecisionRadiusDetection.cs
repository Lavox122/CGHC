using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decision Radius Detection", fileName = "DecisionRadiusDetection")]
public class DecisionRadiusDetection : AIDecision
{
    public float radius = 4f;
    public LayerMask playerMask;
    private Collider2D playerCollider;

    public override bool Decide(StateController controller)
    {
        return DetectPlayer(controller);
    }

    // Returns if the object detected the player
    private bool DetectPlayer(StateController controller)
    {
        playerCollider = Physics2D.OverlapCircle(controller.transform.position, radius, playerMask);
        controller.SetRediusDetectionValues(radius, controller.transform.position, playerCollider);

        if (playerCollider)
        {
            controller.Target = playerCollider.GetComponent<PlayerMotor>();
            return true;
        }

        controller.Target = null;
        return false;
    }
}
