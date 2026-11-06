using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class CatMovement : MonoBehaviour, IMoveable, IJumper, IImpulsable
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float slopeMagnetism;
    [SerializeField] private float slopeCheckRayLength = 5f;

    [SerializeField] private Cat _chosenCharacter;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private MovementStatsMultipliers _statsMultipliers;
    [SerializeField] private MovementStatus _status;

    /*
        ==========================
                Properties
        ==========================
    */

    public Vector2 MovementInput { get; set; } = Vector2.zero;
    public Vector2 HorizontalDirection2 => new Vector2(PlayerRb.transform.forward.x, PlayerRb.transform.forward.z).normalized;
    public Vector3 HorizontalDirection3 => new Vector3(PlayerRb.transform.forward.x, 0f, PlayerRb.transform.forward.z);

    public Cat ChosenCharacter
    {
        get => _chosenCharacter;
        private set => _chosenCharacter = value ?? throw new NullReferenceException("CatCharacter was asigned a null reference.");
    }

    public Rigidbody PlayerRb
    {
        get => _playerRb;
        private set => _playerRb = value ?? throw new NullReferenceException("Rigidbody component was asigned a null reference.");
    }

    public MovementStatsMultipliers StatsMultipliersComponent
    {
        get => _statsMultipliers;
        private set => _statsMultipliers = value ?? throw new NullReferenceException("StatsMultipliers component was asigned a null reference.");
    }

    public MovementStatus StatusComponent
    {
        get => _status;
        private set => _status = value ?? throw new NullReferenceException("Status component was asigned a null reference.");
    }


    /*
         ===========================
                Basic movement
         ===========================
    */

    public void MoveTowards(Vector3 movement)
    {
        Vector3 targetPosition = PlayerRb.position + movement;
        PlayerRb.MovePosition(targetPosition);
    }

    public void Teleport(Vector3 destination)
    {
        PlayerRb.position = destination;
    }

    public void Move(Vector2 input, float maxSpeed, float acceleration, float airAcceleration)
    {
        if (!StatusComponent.CanMove || StatusComponent.IsStunned)
            return;

        Vector3 desiredDir = new Vector3(input.x, 0, input.y).normalized;

        Vector3 velocity = PlayerRb.velocity;
        Vector3 horizontalVel = new Vector3(velocity.x, 0, velocity.z);

        Vector3 targetVelocity = desiredDir * maxSpeed;

        Vector3 velocityDiff = targetVelocity - horizontalVel;

        float accel = StatusComponent.IsGrounded ? acceleration : airAcceleration;

        if (input != Vector2.zero || StatusComponent.IsGrounded)
            PlayerRb.AddForce(velocityDiff * accel, ForceMode.Acceleration);
        else if (!StatusComponent.IsGrounded)
            PlayerRb.AddForce(velocityDiff * airAcceleration, ForceMode.Acceleration);
    }

    public void HandleRotation()
    {
        if (!StatusComponent.CanMove || StatusComponent.IsStunned || MovementInput == Vector2.zero) return;

        Vector3 direction = new Vector3(MovementInput.x, 0f, MovementInput.y);

        //Implement joystick deadzone for rotation
        //if (direction.magnitude < 0.1f) return;

        Quaternion rotation = Quaternion.LookRotation(direction);
        PlayerRb.MoveRotation(rotation);
    }

    public void Jump(float jumpForce)
    {
        if (!StatusComponent.CanJump || StatusComponent.IsStunned)
            return;

        float characterMass = ChosenCharacter.movementStats.mass;

        if (StatusComponent.IsGrounded || (ChosenCharacter.movementCapabilities.canDoubleJump && !StatusComponent.HasDoubleJumped))
        {
            Vector3 verticalVelocity = new Vector3(0, PlayerRb.velocity.y, 0);
            PlayerRb.velocity -= verticalVelocity;
            PlayerRb.AddForce(Vector3.up * jumpForce * characterMass, ForceMode.Impulse);

            if (!StatusComponent.IsGrounded)
            {
                StatusComponent.HasDoubleJumped = true;
            }
        }
    }

    /*
        ======================
                Forces
        ======================
    */

    public void Impulse(Vector3 direction, float magnitude)
    {
        if (StatusComponent.IsImmovable || !ChosenCharacter.movementCapabilities.isKnockbackable)
            return;

        float characterMass = ChosenCharacter.movementStats.mass;
        PlayerRb.AddForce(direction.normalized * magnitude, ForceMode.Impulse);
    }

    public void VelocityChange(Vector3 direction, float magnitude)
    {
        if (StatusComponent.IsImmovable)
            return;

        PlayerRb.AddForce(direction * magnitude, ForceMode.VelocityChange);
    }

    public void Acceleration(Vector3 direction, float magnitude)
    {
        if (StatusComponent.IsImmovable)
            return;

        PlayerRb.AddForce(direction * magnitude, ForceMode.Acceleration);
    }

    /*
        ==================================
               Other Update methods
        ==================================
    */

    private void GroundCheck()
    {
        Collider[] hits = Physics.OverlapSphere(
                groundCheck.position,
                0.15f,
                groundLayer
            );

        UpdateMovementStatus(hits);
    }

    private void UpdateMovementStatus(Collider[] hits)
    {
        StatusComponent.IsGrounded = hits.Length > 0;
        if (StatusComponent.HasDoubleJumped)
            StatusComponent.HasDoubleJumped = !(hits.Length > 0);
    }

    private void CounterGravityInSlopes()
    {
        RaycastHit hit;
        Physics.Raycast(PlayerRb.position, Vector3.down, out hit, slopeCheckRayLength, groundLayer);

        Vector3 normal = hit.normal;
        float slopeAngle = Vector3.Angle(normal, Vector3.up);
        bool isSlope = slopeAngle > 0.1f;

        if (isSlope && StatusComponent.IsGrounded)
        {
            PlayerRb.AddForce(-Physics.gravity, ForceMode.Force);
            PlayerRb.AddForce(-normal * slopeMagnetism, ForceMode.Acceleration);
        }
    }

    /*
        ===========================
               MonoBehaviour
        ===========================
    */

    void Awake()
    {

    }

    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        PlayerRb.mass = ChosenCharacter.movementStats.mass;
        PlayerRb.freezeRotation = true;
        groundCheck = transform.Find("groundcheck");
        StatsMultipliersComponent = GetComponent<MovementStatsMultipliers>();
        StatusComponent = GetComponent<MovementStatus>();

        StatsMultipliersComponent.Initialize();
        StatusComponent.Initialize();
    }

    void FixedUpdate()
    {
        Move(MovementInput, ChosenCharacter.movementStats.acceleration, ChosenCharacter.movementStats.maxSpeed, ChosenCharacter.movementStats.airAcceleration);
        HandleRotation();
        GroundCheck();
        CounterGravityInSlopes();
    }
}