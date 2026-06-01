using System.Collections;
using UnityEngine;

public class CombatStunEffect : CombatEffect
{
    public override void Apply(EffectManager effectManager)
    {
        targetCombatComponent.StatusComponent.CanUseBasicAttacks = false;
        targetCombatComponent.StatusComponent.CanUseAbilities = false;

        effectInstanceCoroutine = effectManager.StartCoroutine(EffectCoroutine(effectManager));
    }

    public override void Remove(EffectManager effectManager)
    {
        if (effectInstanceCoroutine != null)
        {
            effectManager.StopCoroutine(effectInstanceCoroutine);
            effectInstanceCoroutine = null;
        }

        targetCombatComponent.StatusComponent.CanUseBasicAttacks = true;
        targetCombatComponent.StatusComponent.CanUseAbilities = true;
    }

    public override IEnumerator EffectCoroutine(EffectManager effectManager)
    {
        yield return new WaitForSeconds(Duration);

        effectManager.CleanEffect(this);
    }

    public CombatStunEffect(EffectData effData, float duration, CatCombat combatComponentFromTarget) : base(effData, duration, combatComponentFromTarget) { }
}