using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    // Override to add curstom behaviour
    public abstract void Act(StateController controller);
}
