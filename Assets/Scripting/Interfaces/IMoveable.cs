using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public void Move(Vector2 input, float maxSpeed, float acceleration, float airAcceleration);
}
