using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu]
public class HealthStats : ScriptableObject
{
    public float baseHealth;

    public void OnValidate()
    {
        if (baseHealth <= 0) baseHealth = 1f;
    }
}