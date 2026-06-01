using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatInputEvents
{
    public event Action<Abilities> OnPressed;
    public event Action<Abilities> OnReleased;

    public void NotifyPressed(Abilities ability)
    {
        OnPressed?.Invoke(ability);
    }

    public void NotifyReleased(Abilities ability)
    {
        OnReleased?.Invoke(ability);
    }
}