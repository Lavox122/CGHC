using System.Collections;
using UnityEngine;

public class PlayerMovement : PlayerStates
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeedMultiplier = 2f; // Speed multiplier when dashing
    [SerializeField] private float dashDuration = 0.3f; // Dash duration
    [SerializeField] private float dashCooldown = 1f; // Cooldown before dashing again

    public float Speed { get; set; }
    public float InitialSpeed => speed;

    private float _horizontalMovement;
    private float _movement;

    private bool _canDash = true;
    private bool _isDashing = false;
    private float _originalSpeed;

    private int _idleAnimatorParameter = Animator.StringToHash("Idle");
    private int _runAnimatorParameter = Animator.StringToHash("Run");
    private int _dashAnimatorParameter = Animator.StringToHash("Dash");

    protected override void InitState()
    {
        base.InitState();
        Speed = speed;
        _originalSpeed = speed;
    }

    public override void ExecuteState()
    {
        MovePlayer();
        HandleDash();
    }

    // Moves our Player    
    private void MovePlayer()
    {
        if (Mathf.Abs(_horizontalMovement) > 0.1f)
        {
            _movement = _horizontalMovement;
        }
        else
        {
            _movement = 0f;
        }

        float moveSpeed = _movement * Speed;
        moveSpeed = EvaluateFriction(moveSpeed);

        _playerController.SetHorizontalForce(moveSpeed);
    }

    // Initialize our internal movement direction   
    protected override void GetInput()
    {
        _horizontalMovement = _horizontalInput;
    }

    public override void SetAnimation()
    {
        _animator.SetBool(_idleAnimatorParameter, _horizontalMovement == 0 && _playerController.Conditions.IsCollidingBelow);
        _animator.SetBool(_runAnimatorParameter, Mathf.Abs(_horizontalInput) > 0.1f && _playerController.Conditions.IsCollidingBelow);
        _animator.SetBool(_dashAnimatorParameter, _isDashing);
    }

    private float EvaluateFriction(float moveSpeed)
    {
        if (_playerController.Friction > 0)
        {
            moveSpeed = Mathf.Lerp(_playerController.Force.x, moveSpeed, Time.deltaTime * 10f * _playerController.Friction);
        }

        return moveSpeed;
    }

    // Handles Dash Input
    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        Speed = _originalSpeed * dashSpeedMultiplier;
        _animator.SetTrigger(_dashAnimatorParameter);

        yield return new WaitForSeconds(dashDuration);

        Speed = _originalSpeed;
        _isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }
}
