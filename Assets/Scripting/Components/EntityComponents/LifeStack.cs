using System.Collections;
using UnityEngine;

public class LifeStack : MonoBehaviour
{
    [SerializeField] private int _lifeStackSize;
    [SerializeField] private int _currentLifeStackIndex;

    public LifeStackEvents Events { get; private set; } = new LifeStackEvents();

    public int LifeStackSize
    {
        get => _lifeStackSize;
        set => _lifeStackSize = value <= 0 ? 1 : value;
    }

    public int CurrentLifeStackIndex
    {
        get => _currentLifeStackIndex;
        set
        {
            _currentLifeStackIndex = Mathf.Clamp(value, 0, LifeStackSize);
            Events.NotifyLifeStackIndexChanged(CurrentLifeStackIndex);
        }
    }

    public void Initialize(LifeStackStats stats)
    {
        LifeStackSize = stats.lifeStackSize;
        CurrentLifeStackIndex = LifeStackSize;
    }
}