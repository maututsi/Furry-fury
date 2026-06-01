using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Scratch : Ability
{
    public float damage;
    public float hitboxRadius;
    public float directionMagnetismThreshold;
    public float hitstunDuration;
    public float knockbackMagnitude;
    public EffectData combatHitstunData;
    public EffectData movementHitstunData;
    public LayerMask playerLayer;

    public override void Execute(InGameContext userContext)
    {
        Transform attackPoint = userContext.gameObject.transform.Find("lightattackpoint");

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
