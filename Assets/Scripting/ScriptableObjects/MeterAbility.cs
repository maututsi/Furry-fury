using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class MeterAbility : Ability
{
    public float minimumChargeForExecution;

    public void OnValidate()
    {
        minimumChargeForExecution = Mathf.Clamp01(minimumChargeForExecution);
    }
}