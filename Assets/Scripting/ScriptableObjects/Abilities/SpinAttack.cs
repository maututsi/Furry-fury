using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpinAttack : Ability
{
    public float radius;
    public float duration;
    public float effectTickInterval;
    public float degreesPerSecond;
    public float receivedDamageMultiplier;
    public float damage;
    public float knockbackMagnitude;
    public EffectData combatStunData;
    public EffectData statChangeData;
    public LayerMask playerLayer;

    public override void Execute(InGameContext userContext)
    {
        userContext.StartCoroutine(Attack(userContext, duration, radius, effectTickInterval, damage, knockbackMagnitude));

        CombatStunEffect comStun = new CombatStunEffect(combatStunData, duration, userContext.CombatComponent);
        userContext.EffectManager.ApplyEffect(comStun);
        StatChangeEffect buff = new StatChangeEffect(statChangeData, duration, CombatStats.ReceivedDamage, receivedDamageMultiplier, userContext.CombatComponent);
        userContext.EffectManager.ApplyEffect(buff);
    }

    public IEnumerator Attack(InGameContext userContext, float seconds, float effectRadius, float tickInterval, float damagePerTick, float knockback)
    {
        float elapsedTime = 0f;
        Collider thisCollider = userContext.GetComponent<Collider>();

        while (elapsedTime < seconds)
        {
            Collider[] hits = Physics.OverlapSphere(userContext.MovementComponent.PlayerRb.position, effectRadius, playerLayer);
            HitboxDebugger hitboxDebugger = new HitboxDebugger();
            hitboxDebugger.ShowSphere(userContext.MovementComponent.PlayerRb.position, effectRadius, 0.1f);

            foreach (Collider hit in hits)
            {
                if (hit == thisCollider) continue;

                InGameContext targetContext = hit.GetComponent<InGameContext>();
                Vector3 dirToTarget = (targetContext.MovementComponent.PlayerRb.position - userContext.MovementComponent.PlayerRb.position).normalized;

                targetContext.MovementComponent.Impulse(dirToTarget + new Vector3(0f, 0.25f, 0f), knockback);
                targetContext.CombatComponent.TakeDamage(damagePerTick);
            }

            elapsedTime += tickInterval;
            yield return new WaitForSeconds(tickInterval);
        }
    }
}