using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class LifeStackStats : ScriptableObject
{
    public int lifeStackSize;

    public void OnValidate()
    {
        if (lifeStackSize < 0) lifeStackSize = 0;
    }
}