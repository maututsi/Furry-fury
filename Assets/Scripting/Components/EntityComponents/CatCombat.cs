using System.Collections;
using System;
using UnityEngine;

public class CatCombat: MonoBehaviour, IKillable, IRespawnable
{
    [SerializeField] private Cat _chosenCharacter;
    [SerializeField] private Health _health;
    [SerializeField] private LifeStack _lifeStack;
    [SerializeField] private CombatStatsMultipliers _statsMultipliers;
    [SerializeField] private CombatStatus _status;

    private CatMovement movementComponent;

    public Cat ChosenCharacter
    {
        get => _chosenCharacter;
        private set => _chosenCharacter = value ?? throw new NullReferenceException("CatCharacter was asigned a null reference.");
    }

    public Health HealthComponent
    {
        get => _health;
        private set => _health = value ?? throw new NullReferenceException("Health component was asigned a null reference.");
    }

    public LifeStack LifeStackComponent
    {
        get => _lifeStack;
        private set => _lifeStack = value ?? throw new NullReferenceException("LifeStack component was asigned a null reference.");
    }

    public CombatStatsMultipliers StatsMultipliersComponent
    {
        get => _statsMultipliers;
        private set => _statsMultipliers = value ?? throw new NullReferenceException("StackMultipliers component was asigned a null reference.");
    }

    public CombatStatus StatusComponent
    {
        get => _status;
        private set => _status = value ?? throw new NullReferenceException("CombatStatus component was asigned a null reference.");
    }

    public void TakeDamage(float damage)
    {
        if (!StatusComponent.IsAlive || StatusComponent.IsInvulnerable || !ChosenCharacter.combatCapabilities.isDamageable)
            return;

        HealthComponent.TakeDamage(damage * StatsMultipliersComponent.ReceivedDamageMultiplier);

        if (HealthComponent.CurrentHealth == 0)
            HandleDeath();
    }

    public void Heal(float healing)
    {
        if (!StatusComponent.IsAlive || StatusComponent.HasAntiheal || !ChosenCharacter.combatCapabilities.isHealable)
            return;

        HealthComponent.Heal(healing * StatsMultipliersComponent.ReceivedHealingMultiplier);
    }

    public void HandleDeath()
    {
        if (!StatusComponent.IsAlive)
            return;

        StatusComponent.IsAlive = false;
        LifeStackComponent.CurrentLifeStackIndex--;

        if (LifeStackComponent.CurrentLifeStackIndex > 0)
        {
            StartCoroutine(RespawnCoroutine(new Vector3(-0.5f, 52.26f, 5f), 3f));
        }
    }

    public void Respawn(Vector3 respawnPosition)
    {
        HealthComponent.Revive();
        StatusComponent.IsAlive = true;
        movementComponent.Teleport(respawnPosition);
    }

    public IEnumerator RespawnCoroutine(Vector3 respawnPos, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Respawn(respawnPos);
    }

    void Awake()
    {
        movementComponent = GetComponent<CatMovement>();

        HealthComponent = GetComponent<Health>();
        LifeStackComponent = GetComponent<LifeStack>();
        StatsMultipliersComponent = GetComponent<CombatStatsMultipliers>();
        StatusComponent = GetComponent<CombatStatus>();
    }

    void Start()
    {
        HealthComponent.Initialize(ChosenCharacter.healthStats);
        LifeStackComponent.Initialize(ChosenCharacter.lifeStackStats);
        StatsMultipliersComponent.Initialize();
        StatusComponent.Initialize();
    }
}