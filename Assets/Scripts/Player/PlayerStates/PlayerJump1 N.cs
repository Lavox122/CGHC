using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerJump : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private int maxJumps = 2;

    private int _jumpAnimatorParameter = Animator.StringToHash("Jump");
    private int _doubleJumpParameter = Animator.StringToHash("DoubleJump");
    private int _fallAnimatorParameter = Animator.StringToHash("Fall");

    // Return how many jumps we have left
    public int JumpsLeft { get; set; }

    protected override void InitState()
    {
        base.InitState();
        JumpsLeft = maxJumps;
    }

    public override void ExecuteState()
    {
        if (_playerController.Conditions.IsCollidingBelow && _playerController.Force.y == 0f)
        {
            JumpsLeft = maxJumps;
            _playerController.Conditions.IsJumping = false;
        }
    }

    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!CanJump())
        {
            return;
        }

        if (JumpsLeft == 0)
        {
            return;
        }

        JumpsLeft -= 1;

        float jumpForce = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(_playerController.Gravity));
        _playerController.SetVerticalForce(jumpForce);
        _playerController.Conditions.IsJumping = true;
        SoundManager.Instance.PlaySound(AudioLibrary.Instance.JumpClip);
    }

    private bool CanJump()
    {
        if (!_playerController.Conditions.IsCollidingBelow && JumpsLeft <= 0)
        {
            return false;
        }

        if (_playerController.Conditions.IsCollidingBelow && JumpsLeft <= 0)
        {
            return false;
        }

        return true;
    }

    public override void SetAnimation()
    {
        // Jump
        _animator.SetBool(_jumpAnimatorParameter, _playerController.Conditions.IsJumping
                                                  && !_playerController.Conditions.IsCollidingBelow
                                                  && JumpsLeft > 0
                                                  && !_playerController.Conditions.IsFalling
                                                  && !_playerController.Conditions.IsJetpacking);

        // Double jump
        _animator.SetBool(_doubleJumpParameter, _playerController.Conditions.IsJumping
                                                  && !_playerController.Conditions.IsCollidingBelow
                                                  && JumpsLeft == 0
                                                  && !_playerController.Conditions.IsFalling
                                                  && !_playerController.Conditions.IsJetpacking);

        // Fall
        _animator.SetBool(_fallAnimatorParameter, _playerController.Conditions.IsFalling
                                                  && _playerController.Conditions.IsJumping
                                                  && !_playerController.Conditions.IsCollidingBelow
                                                  && !_playerController.Conditions.IsJetpacking);
    }

    private void JumpResponse(float jump)
    {
        _playerController.SetVerticalForce(Mathf.Sqrt(2f * jump * Mathf.Abs(_playerController.Gravity)));
    }

    private void OnEnable()
    {
        Jumper.OnJump += JumpResponse;
    }

    private void OnDisable()
    {
        Jumper.OnJump -= JumpResponse;
    }
}
