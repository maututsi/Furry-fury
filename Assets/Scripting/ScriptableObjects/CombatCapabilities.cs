using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CombatCapabilities : ScriptableObject
{
    public bool isKillable;
    public bool isDamageable;
    public bool isHealable;
    public bool isStunnable;
    public float maxBlockTime;
}