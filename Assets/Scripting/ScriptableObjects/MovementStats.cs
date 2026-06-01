using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class MovementStats : ScriptableObject
{
    public float maxSpeed;
    public float acceleration;
    public float airAcceleration;
    public float jumpForce;
    public float mass;

    public void OnValidate()
    {
        if (maxSpeed < 0) maxSpeed = 0f;
        if (jumpForce < 0) jumpForce = 0f;
        if (mass < 0) mass = 0f;
    }
}