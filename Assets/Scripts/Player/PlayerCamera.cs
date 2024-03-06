using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField, ReadOnly] public Camera Camera;

    [Header("Camera Settings")]
    [SerializeField, Range(0.1f, 10f), Tooltip("The sensitivity of the mouse on the X axis")] public float XSensitivity = 2f;
    [SerializeField, Range(0.1f, 10f), Tooltip("The sensitivity of the mouse on the Y axis")] public float YSensitivity = 2f;
    [SerializeField, Range(-180f, 0f), Tooltip("The minimum value for the X axis rotation")] public float LowerBoundary = -90f;
    [SerializeField, Range(0f, 180f), Tooltip("The maximum value for the X axis rotation")] public float UpperBoundary = 90f;

    [Header("Hidden Attributes")]
    [SerializeField, ReadOnly] private Vector2 _rotation = Vector2.zero;

    void Start()
    {
        Camera = GetComponentInChildren<Camera>();

        //Locks the cursor to the center of the screen and makes it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerRotation();
        CameraRotation();
    }

    private void PlayerRotation()
    {
        //Rotate Player on the Y axis
        _rotation.y += UnityEngine.Input.GetAxisRaw("Mouse X") * XSensitivity * 100f * Time.deltaTime;
        //Apply the rotation to the player
        transform.rotation = Quaternion.Euler(0, _rotation.y, 0);
    }

    private void CameraRotation()
    {
        //Rotate Camera on the X axis
        _rotation.x -= UnityEngine.Input.GetAxis("Mouse Y") * YSensitivity * 100f * Time.deltaTime;
        //Clamp the rotation to the min and max values
        _rotation.x = Mathf.Clamp(_rotation.x, LowerBoundary, UpperBoundary);
        //Apply the rotation to the camera
        Camera.transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
    }
    
    
}
