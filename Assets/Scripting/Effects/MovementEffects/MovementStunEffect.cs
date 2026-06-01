using System.Collections;
using UnityEngine;

public class MovementStunEffect : MovementEffect
{
    public override void Apply(EffectManager effectManager)
    {
        targetMovementComponent.StatusComponent.IsStunned = true;

        effectInstanceCoroutine = effectManager.StartCoroutine(EffectCoroutine(effectManager));
    }

    public override void Remove(EffectManager effectManager)
    {
        if (effectInstanceCoroutine != null)
        {
            effectManager.StopCoroutine(effectInstanceCoroutine);
            effectInstanceCoroutine = null;
        }

        targetMovementComponent.StatusComponent.IsStunned = false;
    }

    public override IEnumerator EffectCoroutine(EffectManager effectManager)
    {
        yield return new WaitForSeconds(Duration);

        effectManager.CleanEffect(this);
    }

    public MovementStunEffect(EffectData effData, float duration, CatMovement movementComponentFromTarget) : base(effData, duration, movementComponentFromTarget) { }
}