using System.Collections;
using UnityEngine;

public interface IStunnable
{
    bool IsUnstoppable { get; }

    void ApplyStun(float seconds);
}