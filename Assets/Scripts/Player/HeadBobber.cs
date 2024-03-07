using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    [SerializeField, ReadOnly] public Camera PlayerCam;
    [SerializeField, ReadOnly] public Movement Movement;

    [Header("Bobbing Settings")]
    [SerializeField] public float CrouchBobbingSpeed = 10f;
    [SerializeField] public float WalkBobbingSpeed = 20f;
    [SerializeField] public float SprintBobbingSpeed = 40f;
    [SerializeField] public float BobbingAmount = 0.05f;

    [Header("Hidden Attributes")]
    [SerializeField, ReadOnly] private float _defaultPosY;
    [SerializeField, ReadOnly] private float _timer = 0.0f;
    [SerializeField, ReadOnly] private float _bobbingSpeed;

    void Start()
    {
        PlayerCam = GetComponentInChildren<Camera>();
        Movement = GetComponent<Movement>();
        _defaultPosY = PlayerCam.transform.localPosition.y;
    }


    void Update()
    {
        _bobbingSpeed = Movement.PlayerState == PlayerState.Crouching ? CrouchBobbingSpeed : Movement.PlayerState == PlayerState.Sprinting ? SprintBobbingSpeed : WalkBobbingSpeed;
        if ((Mathf.Abs(Movement.Direction.x) > 0.1f || Mathf.Abs(Movement.Direction.z) > 0.1f) && Movement.IsGrounded)
        {
            //Player is moving
            _timer += Time.deltaTime * _bobbingSpeed;
            PlayerCam.transform.localPosition = new Vector3(PlayerCam.transform.localPosition.x, _defaultPosY + Mathf.Sin(_timer) * BobbingAmount, PlayerCam.transform.localPosition.z);
        }
        else
        {
            //Idle
            _timer = 0;
            PlayerCam.transform.localPosition = new Vector3(PlayerCam.transform.localPosition.x, Mathf.Lerp(PlayerCam.transform.localPosition.y, _defaultPosY, Time.deltaTime * WalkBobbingSpeed), PlayerCam.transform.localPosition.z);
        }
    }
}
