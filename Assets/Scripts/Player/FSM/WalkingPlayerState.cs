using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelDestruction;

public class WalkingPlayerState : PlayerState
{
    #region Variables
    private PlayerStateManager _player;
    #endregion

    #region Inherited Functions
    public override void StateStart(PlayerStateManager player)
    {
        _player = player;

        _player.ReadyToJump = true;

        _player.Collider.height = _player.DefaultHeight;
    }
    public override void StateUpdate()
    {
        StateHandler();

        JumpHandler();

        MoveHandler();
        SpeedController();

        HeadBobHandler();
    }
    public override void OnCollisionEnter(Collision collision)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Movement Functions
    private void MoveHandler()
    {
        // Set the direction of the _player
        _player.Direction = _player.transform.forward * _player.VerticalInput + _player.transform.right * _player.HorizontalInput;

        // Apply the force to the _player
        if (_player.IsGrounded)
            _player.Rigidbody.AddForce(_player.Direction.normalized * _player.WalkSpeed * 10f, ForceMode.Force);
        else
            _player.Rigidbody.AddForce(_player.Direction.normalized * _player.WalkSpeed * 10f * _player.AirMultiplier, ForceMode.Force);
    }
    private void SpeedController()
    {
        // Get _player current movement speed
        Vector3 _flatVelocity = new Vector3(_player.Rigidbody.velocity.x, 0f, _player.Rigidbody.velocity.z);
        //Check if _player is moving faster than maximum movespeed
        if (_flatVelocity.magnitude > _player.WalkSpeed)
        {
            // Limit _player movespeed
            Vector3 _limitedVelocity = _flatVelocity.normalized * _player.WalkSpeed;
            _player.Rigidbody.velocity = new Vector3(_limitedVelocity.x, _player.Rigidbody.velocity.y, _limitedVelocity.z);
        }

        // Apply drag if _player is on ground
        if (_player.IsGrounded)
            _player.Rigidbody.drag = _player.GroundDrag;
        else
            _player.Rigidbody.drag = 0f;
    }
    #endregion

    #region Jump Functions
    private void JumpHandler()
    {
        if (Input.GetKey(_player.JumpKey) && _player.ReadyToJump && _player.IsGrounded)
        {
            // Set Jump condition to false
            _player.ReadyToJump = false;

            // Perform Jump
            _player.Rigidbody.velocity = new Vector3(_player.Rigidbody.velocity.x, 0f, _player.Rigidbody.velocity.z);
            _player.Rigidbody.AddForce(_player.transform.up * _player.JumpForce, ForceMode.Impulse);

            // Repeat Jump if Jump Key is being held down
            _player.Invoke("ResetJump", _player.JumpCD);
        }
    }
    #endregion

    #region Head Bobbing Functions
    private void HeadBobHandler()
    {
        if ((Mathf.Abs(_player.Direction.x) > 0.1f || Mathf.Abs(_player.Direction.z) > 0.1f) && _player.IsGrounded)
        {
            //_player is moving
            _player.Timer += Time.deltaTime * _player.WalkBobbingSpeed;
            _player.PlayerCam.transform.localPosition = new Vector3(_player.PlayerCam.transform.localPosition.x, _player.DefaultPosY + Mathf.Sin(_player.Timer) * _player.BobbingAmount, _player.PlayerCam.transform.localPosition.z);
        }
        else
        {
            //Idle
            _player.Timer = 0;
            _player.PlayerCam.transform.localPosition = new Vector3(_player.PlayerCam.transform.localPosition.x, Mathf.Lerp(_player.PlayerCam.transform.localPosition.y, _player.DefaultPosY, Time.deltaTime * _player.WalkBobbingSpeed), _player.PlayerCam.transform.localPosition.z);
        }
    }
    #endregion

    #region State Functions
    private void StateHandler()
    {
        if (Input.GetKeyDown(_player.CrouchKey))
            _player.SetState(_player.CrouchingState);
        else if (Input.GetKeyDown(_player.SprintKey))
            _player.SetState(_player.SprintingState);
    }
    #endregion
}
