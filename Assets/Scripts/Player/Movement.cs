using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking,
    Sprinting,
    Crouching,
    Jumping,
    Falling
}

public class Movement : MonoBehaviour
{
    [SerializeField, ReadOnly] public Rigidbody Rigidbody;
    [SerializeField, ReadOnly] public CapsuleCollider Collider;
    [SerializeField, ReadOnly] public LayerMask GroundLayer;

    [Header("Toggle Features")]
    [SerializeField] public bool EnableSprint = true;
    [SerializeField] public bool EnableCrouch = true;
    [SerializeField] public bool EnableJump = true;

    [Header("Movement Settings")]
    [SerializeField, Range(0.1f, 20f), Tooltip("The speed of the player while crouching")] public float CrouchSpeed = 5f;
    [SerializeField, Range(0.1f, 20f), Tooltip("The speed of the player while walking")] public float WalkSpeed = 10f;
    [SerializeField, Range(0.1f, 20f), Tooltip("The speed of the player while sprinting")] public float SprintSpeed = 20f;
    [SerializeField] public float GroundDrag = 6f;

    [Header("Jump Settings")]
    [SerializeField] public float JumpForce = 12f;
    [SerializeField] public float JumpCD = 0.25f;
    [SerializeField] public float AirMultiplier = 0.4f;

    [Header("Keybinding Settings")]
    [SerializeField] public bool ToggleSprint = true;
    [SerializeField] public bool ToggleCrouch = true;
    [SerializeField, Tooltip("Crouch Key")] public KeyCode CrouchKey = KeyCode.LeftControl;
    [SerializeField, Tooltip("Sprint Key")] public KeyCode SprintKey = KeyCode.LeftShift;
    [SerializeField, Tooltip("Jump Key")] public KeyCode JumpKey = KeyCode.Space;

    [Header("Hidden Attributes")]
    [SerializeField, ReadOnly] public PlayerState PlayerState = PlayerState.Idle;
    [SerializeField, ReadOnly] private float _horizontalInput;
    [SerializeField, ReadOnly] private float _verticalInput;
    [SerializeField, ReadOnly] private float _movespeed;
    [SerializeField, ReadOnly] private Vector3 _direction;
    [SerializeField, ReadOnly] private bool _isGrounded;
    [SerializeField, ReadOnly] private bool _readyToJump;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        GroundLayer = LayerMask.GetMask("Ground");

        _readyToJump = true;
    }

    void Update()
    {
        _isGrounded = CheckGrounded();
        InputHandler();
        SpeedController();
    }

    private void FixedUpdate()
    {
        MoveHandler();
    }

    #region Movement Functions
    private void InputHandler()
    {
        // Get the input from the player
        _horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        _verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");

        // Check if conditions to jump is met
        if (Input.GetKey(JumpKey) && _readyToJump && _isGrounded)
        {
            // Perform Jump
            _readyToJump = false;
            JumpHandler();
            // Repeat Jump if Jump Key is being held down
            Invoke(nameof(ResetJump), JumpCD);
        }
    }
    private void MoveHandler()
    {
        // Assign max movespeed
        _movespeed = PlayerState == PlayerState.Crouching ? CrouchSpeed : PlayerState == PlayerState.Sprinting ? SprintSpeed : WalkSpeed;
        // Set the direction of the player
        _direction = transform.forward * _verticalInput + transform.right * _horizontalInput;
        
        // Apply the force to the player
        if (_isGrounded)
            Rigidbody.AddForce(_direction.normalized * _movespeed * 10f, ForceMode.Force);
        else
            Rigidbody.AddForce(_direction.normalized * _movespeed * 10f * AirMultiplier, ForceMode.Force);
    }
    private void SpeedController()
    {
        // Get player current movement speed
        Vector3 _flatVelocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);
        //Check if player is moving faster than maximum movespeed
        if (_flatVelocity.magnitude > _movespeed)
        {
            // Limit player movespeed
            Vector3 _limitedVelocity = _flatVelocity.normalized * _movespeed;
            Rigidbody.velocity = new Vector3(_limitedVelocity.x, Rigidbody.velocity.y, _limitedVelocity.z);
        }

        // Apply drag if player is on ground
        if (_isGrounded)
            Rigidbody.drag = GroundDrag;
        else
            Rigidbody.drag = 0f;
    }
    #endregion

    #region Jump Function
    private void JumpHandler()
    {
        // Perform Jump
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);
        Rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        // Reset Jump condition
        _readyToJump = true;
    }
    #endregion

    private bool CheckGrounded()
    {
        // Perform raycast to determine if player is on ground
        return Physics.Raycast(transform.position, Vector3.down, Collider.height * 0.5f + 0.2f, GroundLayer);
    }
}
