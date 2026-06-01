using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class HeavyAttack : Ability
{
    public float hitboxRadius;
    public float baseDamage;
    public float baseKnockback;
    public float maxChargeTime;
    public float directionMagnetismThreshold;
    public float hitstunDuration;
    public EffectData combatHitstunData;
    public EffectData movementHitstunData;
    public LayerMask playerLayer;

    public override void Execute(InGameContext userContext)
    {
        Abilities thisAbility = userContext.ChosenCharacter.abilitySet.GetAbilityType(this);
        float chargeTime = Mathf.Clamp(userContext.CombatController.HoldingTimes[thisAbility], 0f, maxChargeTime);

        float damage = baseDamage * (9 * (chargeTime / maxChargeTime) + 1);
        float knockbackMagnitude = baseKnockback * (9 * (chargeTime / maxChargeTime) + 1);

        Debug.Log($"Damage: {damage}. Knockback: {knockbackMagnitude}");

        Transform attackPoint = userContext.gameObject.transform.Find("heavyattackpoint");

        Collider[] hits = Physics.OverlapBox(attackPoint.position, Vector3.one * hitboxRadius, Quaternion.identity, playerLayer);
        Collider thisCollider = userContext.GetComponent<Collider>();

        HitboxDebugger debug = userContext.GetComponent<HitboxDebugger>();
        debug.ShowBox(attackPoint.position, Vector3.one * hitboxRadius, 0.5f);

        foreach (Collider hit in hits)
        {
            if (hit == thisCollider) continue;

            InGameContext targetContext = hit.GetComponent<InGameContext>();

            Vector3 difference = DirectionMagnetism(userContext, targetContext);
            userContext.MovementComponent.MoveTowards(difference * 0.9f);

            targetContext.CombatComponent.TakeDamage(damage * userContext.CombatComponent.StatsMultipliersComponent.InflictedDamageMultiplier);

            CombatStunEffect combatStun = new CombatStunEffect(combatHitstunData, hitstunDuration, targetContext.CombatComponent);
            MovementStunEffect movementStun = new MovementStunEffect(movementHitstunData, hitstunDuration, targetContext.MovementComponent);

            targetContext.EffectManager.ApplyEffect(combatStun);
            targetContext.EffectManager.ApplyEffect(movementStun);

            Vector3 knockbackDir = targetContext.MovementComponent.PlayerRb.position - userContext.MovementComponent.PlayerRb.position;
            targetContext.MovementComponent.Impulse(knockbackDir.normalized, knockbackMagnitude);
        }
    }

    private Vector3 DirectionMagnetism(InGameContext userContext, InGameContext targetContext)
    {
        Vector3 userPos = userContext.MovementComponent.PlayerRb.position;
        Vector3 targetPos = targetContext.MovementComponent.PlayerRb.position;

        Vector3 forward = userContext.MovementComponent.HorizontalDirection2;
        Vector3 differenceVector = new Vector3(targetPos.x - userPos.x, 0f, targetPos.z - userPos.z);
        Vector3 direction = differenceVector.normalized;

        float dot = Vector3.Dot(forward, direction);

        if (dot > directionMagnetismThreshold)
        {
            Debug.DrawRay(userPos, direction * 1.5f, Color.green, 4f);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            userContext.MovementComponent.PlayerRb.MoveRotation(targetRotation);
        }

        return differenceVector;
    }
}