using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealth : Collectable
{
    protected override void Collect()
    {
        AddLife();
    }

    // Adds life
    private void AddLife()
    {
        if (_playerMotor.GetComponent<Health>() == null)
        {
            return;
        }

        Health playerHealth = _playerMotor.GetComponent<Health>();
        if (playerHealth.CurrentLifes < playerHealth.MaxLifes)
        {
            playerHealth.AddLife();
        }
    }
}
