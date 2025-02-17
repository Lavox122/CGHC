using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private PlayerStates[] _playerStates;

    private void Start()
    {
        _playerStates = GetComponents<PlayerStates>();
    }

    private void Update()
    {
        if (_playerStates.Length != 0)
        {
            foreach (PlayerStates state in _playerStates)
            {
                state.LocalInput();
                state.ExecuteState();
                state.SetAnimation();
            }
        }
    }

    public void SpawnPlayer(Transform newPosition)
    {
        transform.position = newPosition.position;
    }
}
