using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions
{
    public bool IsCollidingBelow { get; set; }
    public bool IsCollidingAbove { get; set; }
    public bool IsCollidingRight { get; set; }
    public bool IsCollidingLeft { get; set; }
    public bool IsFalling { get; set; }
    public bool IsWallClinging { get; set; }
    public bool IsJetpacking { get; set; }
    public bool IsJumping { get; set; }

    public void Reset()
    {
        IsCollidingBelow = false;
        IsCollidingLeft = false;
        IsCollidingRight = false;
        IsCollidingAbove = false;

        IsFalling = false;
    }
}
