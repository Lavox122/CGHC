using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : PathFollow
{
    public bool CollidingWithPlayer { get; set; }
    public bool loop = true; //to control between looping platform and stopping at the end
    public bool Activated = true; //set to false to make it activate only when player is on it
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        CollidingWithPlayer = true;
        Activated = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CollidingWithPlayer = false;
    }

    protected override void Update()
    {
        if (!Activated && !loop) return;
        if(!loop && ReachedEnd()) return;

        base.Update();
    }

    private bool ReachedEnd()
    {
        return CurrentWaypointNumber() >= PathLength() - 1;
    }
}
