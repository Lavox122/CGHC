using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    protected PlayerController _playerController;
    protected Animator _animator;
    protected float _horizontalInput;
    protected float _verticalInput;

    protected virtual void Start()
    {
        InitState();
    }

    // Here we call some logic we need in start
    protected virtual void InitState()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    // Override in order to create the state logic
    public virtual void ExecuteState()
    {

    }

    // Gets the normal Input   
    public virtual void LocalInput()
    {

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        GetInput();
    }

    // Override to support other Inputs
    protected virtual void GetInput()
    {

    }

    public virtual void SetAnimation()
    {

    }
}
