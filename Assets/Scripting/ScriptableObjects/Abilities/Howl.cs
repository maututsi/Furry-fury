using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Howl : Ability
{
    public float maxRadius;
    public float coneAngle;
    public float knockbackMagnitude;
    public float stunDuration;
    public EffectData movementStunData;
    public EffectData combatStunData;
    public LayerMask playerLayer;

    public override void Execute(InGameContext userContext)
    {
        ViewCone(userContext);

        Collider[] hits = Physics.OverlapSphere(
                userContext.MovementComponent.PlayerRb.position,
                maxRadius,
                playerLayer
            );

        Collider thisCollider = userContext.GetComponent<Collider>();

        foreach (Collider hit in hits)
        {
            if (hit == thisCollider) continue;

            InGameContext targetContext = hit.GetComponent<InGameContext>();
            Vector3 dirToTarget = (targetContext.MovementComponent.PlayerRb.position - userContext.MovementComponent.PlayerRb.position).normalized;

            float angle = Vector3.Angle(
                    userContext.MovementComponent.HorizontalDirection3,
                    dirToTarget
                );

            if (angle <= coneAngle * 0.5f)
            {
                targetContext.MovementComponent.Impulse(dirToTarget + new Vector3(0, 0.5f, 0), knockbackMagnitude);

                MovementStunEffect movStun = new MovementStunEffect(movementStunData, stunDuration, targetContext.MovementComponent);
                CombatStunEffect comStun = new CombatStunEffect(combatStunData, stunDuration, targetContext.CombatComponent);

                targetContext.EffectManager.ApplyEffect(movStun);
                targetContext.EffectManager.ApplyEffect(comStun);
            }
        }
    }

    private void ViewCone(InGameContext userContext)
    {
        Debug.DrawRay(
            userContext.MovementComponent.PlayerRb.position,
            userContext.MovementComponent.HorizontalDirection3 * maxRadius,
            Color.red,
            1f
        );

        Vector3 left = Quaternion.Euler(0, -coneAngle * 0.5f, 0) * userContext.MovementComponent.HorizontalDirection3;

        Vector3 right = Quaternion.Euler(0, coneAngle * 0.5f, 0) * userContext.MovementComponent.HorizontalDirection3;

        Debug.DrawRay(userContext.MovementComponent.PlayerRb.position, left * maxRadius, Color.green, 1f);
        Debug.DrawRay(userContext.MovementComponent.PlayerRb.position, right * maxRadius, Color.green, 1f);
    }
}