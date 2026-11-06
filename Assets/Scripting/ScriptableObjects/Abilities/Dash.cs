using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Dash : Ability
{
    public float impulseMagnitude;

    public override void Execute(InGameContext userContext)
    {
        Vector3 direction = new Vector3(userContext.MovementComponent.HorizontalDirection2.x, 0, userContext.MovementComponent.HorizontalDirection2.y).normalized;
        userContext.MovementComponent.Impulse(direction, impulseMagnitude);
    }
}
