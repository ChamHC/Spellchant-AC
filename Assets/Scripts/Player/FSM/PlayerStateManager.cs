using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelDestruction;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField, ReadOnly] public Rigidbody Rigidbody;
    [SerializeField, ReadOnly] public CapsuleCollider Collider;
    [SerializeField, ReadOnly] public Camera PlayerCam;
    [SerializeField, ReadOnly] public LayerMask GroundLayer;

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
    [SerializeField, ReadOnly] public Vector3 Direction;
    [SerializeField, ReadOnly] public bool IsGrounded;
    [SerializeField, ReadOnly] public float HorizontalInput;
    [SerializeField, ReadOnly] public float VerticalInput;
    [SerializeField, ReadOnly] public bool ReadyToJump;
    [SerializeField, ReadOnly] public float DefaultPosY;
    [SerializeField, ReadOnly] public float Timer = 0.0f;
    [SerializeField, ReadOnly] public bool IsOnSlope;
    [SerializeField, ReadOnly] public RaycastHit slopeHit;

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
