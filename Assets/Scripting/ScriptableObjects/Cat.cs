using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class Cat : ScriptableObject
{
    public int characterId;
    public string catName;

    public CatAssets catAssets;
    public HealthStats healthStats;
    public LifeStackStats lifeStackStats;
    public CombatCapabilities combatCapabilities;
    public MovementStats movementStats;
    public MovementCapabilities movementCapabilities;
    public AbilitySet abilitySet;
}