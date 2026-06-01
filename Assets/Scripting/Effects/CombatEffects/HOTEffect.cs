using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class HOTEffect : CombatEffect
{
    public float Hps;
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
        float healingPerTick = Hps * TickInterval;

        while (Time.time < endingTime)
        {
            targetCombatComponent.Heal(healingPerTick);
            yield return new WaitForSeconds(TickInterval);
        }

        effectManager.CleanEffect(this);
    }

    public HOTEffect(EffectData effData, float duration, float hps, float tickInterval, CatCombat combatComponentFromTarget) : base(effData, duration, combatComponentFromTarget)
    {
        Hps = hps;
        TickInterval = tickInterval;
    }
}