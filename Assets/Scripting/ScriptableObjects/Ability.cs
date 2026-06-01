using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public int id;
    public float cooldown;
    public bool executesWhenPressed;

    public abstract void Execute(InGameContext userContext);
}
