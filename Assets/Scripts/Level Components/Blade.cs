using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : LevelComponent
{
    public override void Damage(PlayerMotor player)
    {
        base.Damage(player);
        Debug.Log("Blade");
    }
}
