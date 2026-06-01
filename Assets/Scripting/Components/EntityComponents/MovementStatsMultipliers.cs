using System.Collections;
using UnityEngine;

public class MovementStatsMultipliers : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _jumpForceMultiplier;

    public float SpeedMultiplier
    {
        get => _speedMultiplier;
        set => _speedMultiplier = value < 0f ? 0f : value;
    }

    public float JumpForceMultiplier
    {
        get => _jumpForceMultiplier;
        set => _jumpForceMultiplier = value < 0f ? 0f : value;
    }

    public void Initialize()
    {
        SpeedMultiplier = 1f;
        JumpForceMultiplier = 1f;
    }
}