using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DOTEffect : CombatEffect
{
    public float Dps;
    public float TickInterval;

    public override void Apply(EffectManager effectManager)
    {
        effectInstanceCoroutine = effectManager.StartCoroutine(EffectCoroutine(effectManager));
    }

    public override void Remove(EffectManager effectManager)
    {
        if (effectInstanceCoroutine != null)
        {
            effectManager.StopCoroutine(effectInstanceCoroutine);
            effectInstanceCoroutine = null;
        }
    }

    public override IEnumerator EffectCoroutine(EffectManager effectManager)
    {
        float endingTime = Time.time + Duration;
        float damagePerTick = Dps * TickInterval;

        while (Time.time < endingTime)
        {
            targetCombatComponent.TakeDamage(damagePerTick);

            yield return new WaitForSeconds(TickInterval);
        }

        effectManager.CleanEffect(this);
    }

    public DOTEffect(EffectData effData, float duration, float dps, float tickInterval, CatCombat combatComponentFromTarget) : base(effData, duration, combatComponentFromTarget)
    {
        Dps = dps;
        TickInterval = tickInterval;
    }
}