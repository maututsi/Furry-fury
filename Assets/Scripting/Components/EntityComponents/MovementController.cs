using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private InGameContext playerContext;
    [SerializeField] private MovementInput movementInput;

    private void HandleMove()
    {
        playerContext.MovementComponent.MovementInput = movementInput.MovementInputVector;
    }

    private void HandleJump()
    {
        if (movementInput.ConsumeJump())
        {
            playerContext.MovementComponent.Jump(playerContext.ChosenCharacter.movementStats.jumpForce);
        }
    }

    private void HandleMovement()
    {
        HandleMove();
        HandleJump();
    }

    void Start()
    {
        playerContext = GetComponent<InGameContext>();
        movementInput = GetComponent<MovementInput>();
    }

    void Update()
    {
        HandleMovement();
    }
}
