using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class Explosion : Ability
{
    public float radius;
    public float knockbackMagnitude;
    public float damage;
    public float dotDuration;
    public float dotDps;
    public float dotTickInterval;
    public EffectData dotData;
    public LayerMask playerLayer;

    public override void Execute(InGameContext userContext)
    {
        Collider[] hits = Physics.OverlapSphere(
                userContext.MovementComponent.PlayerRb.position,
                radius,
                playerLayer
            );

        Collider thisCollider = userContext.GetComponent<Collider>();
        foreach (Collider hit in hits)
        {
            if (hit == thisCollider) continue;

            InGameContext targetContext = hit.GetComponent<InGameContext>();
            Vector3 dirToTarget = (targetContext.MovementComponent.PlayerRb.position - userContext.MovementComponent.PlayerRb.position).normalized;
            targetContext.MovementComponent.Impulse(dirToTarget + new Vector3(0f, 0.5f, 0f), knockbackMagnitude);
            targetContext.CombatComponent.TakeDamage(damage);

            DOTEffect dot = new DOTEffect(dotData, dotDuration, dotDps, dotTickInterval, targetContext.CombatComponent);
            targetContext.EffectManager.ApplyEffect(dot);
        }
    }
}