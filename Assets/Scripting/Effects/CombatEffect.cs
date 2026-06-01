using System.Collections;
using UnityEngine;

public abstract class CombatEffect : Effect
{
    protected CatCombat targetCombatComponent;

    public CombatEffect(EffectData effData, float duration, CatCombat combatComponentFromTarget) : base(effData, duration)
    {
        targetCombatComponent = combatComponentFromTarget;
    }
}