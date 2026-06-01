using System;
using System.Collections;
using UnityEngine;

public class LifeStackEvents
{
    public event Action<int> OnLifeStackIndexChanged;

    public void NotifyLifeStackIndexChanged(int newIndex)
    {
        OnLifeStackIndexChanged?.Invoke(newIndex);
    }
}