using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    public HealthEvents Events { get; private set; } = new HealthEvents();

    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value <= 0 ? 1 : value;
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float HealthPercentage => CurrentHealth / MaxHealth;

    public void Initialize(HealthStats stats)
    {
        MaxHealth = stats.baseHealth;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0f, MaxHealth);
        Events.NotifyDamaged(CurrentHealth, MaxHealth);

        if (CurrentHealth == 0)
            Events.NotifyDeath();
    }

    public void Revive()
    {
        CurrentHealth = MaxHealth;
        Events.NotifyRevive();
    }

    public void Heal(float healing)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healing, 0f, MaxHealth);

        Events.NotifyHealed(CurrentHealth, MaxHealth);
    }
}
