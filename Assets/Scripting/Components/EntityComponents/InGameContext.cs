using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameContext : MonoBehaviour
{
    [SerializeField] private Cat _chosenCharacter;
    [SerializeField] private CombatController _combatController;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private CatCombat _combatComponent;
    [SerializeField] private CatMovement _movementComponent;
    [SerializeField] private EffectManager _effectManager;
    [SerializeField] private ConditionalEffectManager _conditionalEffectManager;
    [SerializeField] private CooldownManager _cooldownManager;

    public Cat ChosenCharacter => _chosenCharacter;
    public CombatController CombatController => _combatController;
    public MovementController MovementController => _movementController;
    public CatCombat CombatComponent => _combatComponent;
    public CatMovement MovementComponent => _movementComponent;
    public EffectManager EffectManager => _effectManager;
    public ConditionalEffectManager ConditionalEffectManager => _conditionalEffectManager;
    public CooldownManager CooldownManager => _cooldownManager;

    void Awake()
    {
        _combatController = GetComponent<CombatController>();
        _movementController = GetComponent<MovementController>();
        _combatComponent = GetComponent<CatCombat>();
        _movementComponent = GetComponent<CatMovement>();
        _effectManager = GetComponent<EffectManager>();
        _conditionalEffectManager = GetComponent<ConditionalEffectManager>();
        _cooldownManager = GetComponent<CooldownManager>();
    }

    /*
        ==============
            Events
        ==============
    */

    void Start()
    {
        CombatComponent.HealthComponent.Events.OnDeath += EffectManager.Cleanse;
    }

    public void OnDisable()
    {
        CombatComponent.HealthComponent.Events.OnDeath -= EffectManager.Cleanse;
    }
}
