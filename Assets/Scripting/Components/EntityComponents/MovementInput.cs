using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInput : MonoBehaviour
{
    public Vector2 MovementInputVector { get; private set; }
    public bool JumpPressed { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInputVector = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpPressed = true;
        }
    }

    public bool ConsumeJump()
    {
        if (!JumpPressed) return false;

        JumpPressed = false;
        return true;
    }
}
