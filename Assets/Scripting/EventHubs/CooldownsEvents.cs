using System;
using System.Collections;
using UnityEngine;

public class CooldownsEvents
{
    public event Action OnCooldownChange;
    public event Action OnCooldownStarted;
    public event Action OnCooldownFinished;

    public void NotifyCooldownChange()
    {
        OnCooldownChange?.Invoke();
    }

    public void NotifyCooldownStarted()
    {
        OnCooldownStarted?.Invoke();
    }

    public void NotifyCooldownFinished()
    {
        OnCooldownFinished?.Invoke();
    }
}