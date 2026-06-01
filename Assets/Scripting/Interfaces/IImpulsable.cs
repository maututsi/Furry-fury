using System.Collections;
using UnityEngine;

public interface IImpulsable
{
    void Impulse(Vector3 direction, float force);
}