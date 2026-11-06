using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChangeEffect : CombatEffect
{
    public float factor;
    public CombatStats stat;

    public override void Apply(EffectManager effectManager)
    {
        targetCombatComponent.StatsMultipliersComponent.MultiplyStat(stat, factor);

        effectInstanceCoroutine = effectManager.StartCoroutine(EffectCoroutine(effectManager));
    }

    public override void Remove(EffectManager effectManager)
    {
        if (effectInstanceCoroutine != null)
        {
            effectManager.StopCoroutine(effectInstanceCoroutine);
            effectInstanceCoroutine = null;
        }

        targetCombatComponent.StatsMultipliersComponent.MultiplyStat(stat, 1 / factor);
    }

    public override IEnumerator EffectCoroutine(EffectManager effectManager)
    {
        yield return new WaitForSeconds(Duration);

        effectManager.CleanEffect(this);
    }

    public StatChangeEffect(EffectData effData, float duration, CombatStats statToChange, float multiplier, CatCombat combatComponentFromTarget)
        : base(effData, duration, combatComponentFromTarget)
    {
        factor = multiplier == 0f ? 0.01f : multiplier; // Cheap non-zero validation xd
        stat = statToChange;
    }
}
