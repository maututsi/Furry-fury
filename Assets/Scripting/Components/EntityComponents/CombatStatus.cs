using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatStatus : Status
{
    [SerializeField] private bool _isBlocking;
    [SerializeField] private bool _isAlive;
    [SerializeField] private bool _isImmortal;
    [SerializeField] private bool _isInvulnerable;
    [SerializeField] private bool _isUnstoppable;
    [SerializeField] private bool _hasAntiheal;
    [SerializeField] private bool _canUseBasicAttacks;
    [SerializeField] private bool _canUseAbilities;
    
    public bool IsBlocking
    {
        get => _isBlocking;
        set => _isBlocking = value;
    }

    public bool IsAlive
    {
        get => _isAlive;
        set => _isAlive = value;
    }
    public bool IsImmortal
    {
        get => _isImmortal;
        set => _isImmortal = value;
    }
    public bool IsInvulnerable
    {
        get => _isInvulnerable;
        set => _isInvulnerable = value;
    }
    public bool IsUnstoppable
    {
        get => _isUnstoppable;
        set => _isUnstoppable = value;
    }
    public bool HasAntiheal
    {
        get => _hasAntiheal;
        set => _hasAntiheal = value;
    }
    public bool CanUseBasicAttacks
    {
        get => _canUseBasicAttacks;
        set => _canUseBasicAttacks = value;
    }
    public bool CanUseAbilities
    {
        get => _canUseAbilities;
        set => _canUseAbilities = value;
    }

    public void Initialize()
    {
        IsBlocking = false;
        IsAlive = true;
        IsImmortal = false;
        IsInvulnerable = false;
        IsUnstoppable = false;
        HasAntiheal = false;
        CanUseBasicAttacks = true;
        CanUseAbilities = true;
    }
}