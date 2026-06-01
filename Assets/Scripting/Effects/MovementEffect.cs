using System.Collections;
using UnityEngine;

public abstract class MovementEffect : Effect
{
    protected CatMovement targetMovementComponent;

    public MovementEffect(EffectData effData, float duration, CatMovement movementComponentFromTarget) : base(effData, duration)
    {
        targetMovementComponent = movementComponentFromTarget;
    }
}