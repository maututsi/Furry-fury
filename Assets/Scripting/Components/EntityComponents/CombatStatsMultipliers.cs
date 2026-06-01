using System.Collections;
using UnityEngine;

public enum CombatStats
{
    ReceivedDamage = 0,
    InflictedDamage = 1,
    ReceivedHealing = 2,
    InflictedHealing = 3
}

public class CombatStatsMultipliers : MonoBehaviour
{
    [SerializeField] private float _receivedDamageMultiplier;
    [SerializeField] private float _inflictedDamageMultiplier;
    [SerializeField] private float _receivedHealingMultiplier;
    [SerializeField] private float _inflictedHealingMultiplier;

    public float ReceivedDamageMultiplier
    {
        get => _receivedDamageMultiplier;
        set => _receivedDamageMultiplier = value < 0f ? 0f : value;
    }

    public float InflictedDamageMultiplier
    {
        get => _inflictedDamageMultiplier;
        set => _inflictedDamageMultiplier = value < 0f ? 0f : value;
    }

    public float ReceivedHealingMultiplier
    {
        get => _receivedHealingMultiplier;
        set => _receivedHealingMultiplier = value < 0f ? 0f : value;
    }

    public float InflictedHealingMultiplier
    {
        get => _inflictedHealingMultiplier;
        set => _inflictedHealingMultiplier = value < 0f ? 0f : value;
    }

    public void Initialize()
    {
        ReceivedDamageMultiplier = 1f;
        InflictedDamageMultiplier = 1f;
        ReceivedDamageMultiplier = 1f;
        ReceivedHealingMultiplier = 1f;
        InflictedHealingMultiplier = 1f;
    }

    public void MultiplyStat(CombatStats stat, float multiplier)
    {
        switch (stat)
        {
            case CombatStats.ReceivedDamage:
                ReceivedDamageMultiplier *= multiplier;
                break;
            case CombatStats.InflictedDamage:
                InflictedDamageMultiplier *= multiplier;
                break;
            case CombatStats.ReceivedHealing:
                ReceivedHealingMultiplier *= multiplier;
                break;
            case CombatStats.InflictedHealing:
                InflictedHealingMultiplier *= multiplier;
                break;
            default:
                break;
        }
    }
}