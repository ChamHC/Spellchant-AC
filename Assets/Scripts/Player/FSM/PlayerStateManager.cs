using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelDestruction;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] public Rigidbody Rigidbody;
    [SerializeField] public CapsuleCollider Collider;
    [SerializeField] public Camera PlayerCam;
    [SerializeField] public LayerMask GroundLayer;

    [Header("FSM")]
    private PlayerState _currentState;
    public WalkingPlayerState WalkingState = new WalkingPlayerState();
    public SprintingPlayerState SprintingState = new SprintingPlayerState();
    public CrouchingPlayerState CrouchingState = new CrouchingPlayerState();

    [Header("Movement Settings")]
    [SerializeField, Range(0.1f, 20f)] public float CrouchSpeed = 5f;
    [SerializeField, Range(0.1f, 20f)] public float WalkSpeed = 7f;
    [SerializeField, Range(0.1f, 20f)] public float SprintSpeed = 10f;
    [SerializeField] public float GroundDrag = 6f;

    [Header("Jump Settings")]
    [SerializeField] public float JumpForce = 12f;
    [SerializeField] public float JumpCD = 0.25f;
    [SerializeField] public float AirMultiplier = 0.4f;

    [Header("Crouch Settings")]
    [SerializeField] public float DefaultHeight = 2f;
    [SerializeField] public float CrouchHeight = 1f;

    [Header("Head Bobbing Settings")]
    [SerializeField] public float CrouchBobbingSpeed = 10f;
    [SerializeField] public float WalkBobbingSpeed = 12f;
    [SerializeField] public float SprintBobbingSpeed = 15f;
    [SerializeField] public float BobbingAmount = 0.1f;

    [Header("Keybinding Settings")]
    [SerializeField] public bool ToggleSprint = true;
    [SerializeField] public bool ToggleCrouch = true;
    [SerializeField] public KeyCode CrouchKey = KeyCode.LeftControl;
    [SerializeField] public KeyCode SprintKey = KeyCode.LeftShift;
    [SerializeField] public KeyCode JumpKey = KeyCode.Space;

    [Header("Hidden Attributes")]
    [SerializeField] public Vector3 Direction;
    [SerializeField] public bool IsGrounded;
    [SerializeField] public float HorizontalInput;
    [SerializeField] public float VerticalInput;
    [SerializeField] public bool ReadyToJump;
    [SerializeField] public float DefaultPosY;
    [SerializeField] public float Timer = 0.0f;
    [SerializeField] public bool IsOnSlope;
    [SerializeField] public RaycastHit slopeHit;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        PlayerCam = GetComponentInChildren<Camera>();
        GroundLayer = LayerMask.GetMask("Default");

        DefaultPosY = PlayerCam.transform.localPosition.y;

        _currentState = WalkingState;
        _currentState.StateStart(this);
    }

    void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, Collider.height * 0.5f + 0.2f, GroundLayer);
        IsOnSlope = SlopeHandler();

        InputHandler();

        _currentState.StateUpdate();
    }

    public void SetState(PlayerState state)
    {
        _currentState = state;
        _currentState.StateStart(this);
    }

    private void InputHandler()
    {
        // Get the input from the _player
        HorizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        VerticalInput = UnityEngine.Input.GetAxisRaw("Vertical");
    }

    private void ResetJump()
    {
        // Reset Jump condition
        ReadyToJump = true;
    }

    private bool SlopeHandler()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, Collider.height * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < 80f && angle != 0f;
        }

        return false;
    }
}
