using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEvents
{
    public event Action<float, float> OnDamaged;
    public event Action<float, float> OnHealed;
    public event Action OnDeath;
    public event Action OnRevive;

    public void NotifyDamaged(float updatedHealth, float maxHealth)
    {
        OnDamaged?.Invoke(updatedHealth, maxHealth);
    }

    public void NotifyHealed(float updatedHealth, float maxHealth)
    {
        OnHealed?.Invoke(updatedHealth, maxHealth);
    }

    public void NotifyDeath()
    {
        OnDeath?.Invoke();
    }

    public void NotifyRevive()
    {
        OnRevive?.Invoke();
    }
}
