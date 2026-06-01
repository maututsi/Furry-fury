using System.Collections;
using UnityEngine;

public class MovementStatus : Status
{
    [SerializeField] private bool _canMove;
    [SerializeField] private bool _canJump;
    [SerializeField] private bool _canBeKnockbacked;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _hasDoubleJumped;
    [SerializeField] private bool _isGliding;
    [SerializeField] private bool _isImmovable;
    [SerializeField] private bool _isStunned;

    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }
    public bool CanJump
    {
        get => _canJump;
        set => _canJump = value;
    }
    public bool CanBeKnockbacked
    {
        get => _canBeKnockbacked;
        set => _canBeKnockbacked = value;
    }
    public bool IsGrounded
    {
        get => _isGrounded;
        set => _isGrounded = value;
    }
    public bool HasDoubleJumped
    {
        get => _hasDoubleJumped;
        set => _hasDoubleJumped = value;
    }
    public bool IsGliding
    {
        get => _isGliding;
        set => _isGliding = value;
    }
    public bool IsImmovable
    {
        get => _isImmovable;
        set => _isImmovable = value;
    }

    public bool IsStunned
    {
        get => _isStunned;
        set => _isStunned = value;
    }

    public void Initialize()
    {
        CanMove = true;
        CanJump = true;
        CanBeKnockbacked = true;
        IsGrounded = true;
        HasDoubleJumped = false;
        IsGliding = false;
        IsImmovable = false;
        IsStunned = false;
    }
}