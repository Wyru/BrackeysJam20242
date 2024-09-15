using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField]
    public float _sensitivity;

    private Vector2 _mouseInput;

    private float _pitch;
    public InputActionReference _inputs;
    public Transform playerBody;
    private float smoothPitch;

    public bool locked;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (!locked)
        {
            // Read mouse input
            _mouseInput = _inputs.action.ReadValue<Vector2>();

        }

    }

    void FixedUpdate()
    {
        // Rotate the player body along the Y-axis (horizontal movement)
        playerBody.Rotate(Vector3.up, _mouseInput.x * _sensitivity * Time.deltaTime);
        // Handle the pitch (up and down camera movement)
        _pitch -= _mouseInput.y * _sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);

        // Smooth the pitch using Lerp
        smoothPitch = Mathf.Lerp(smoothPitch, _pitch, 0.5f);

        // Apply smoothed pitch to the camera
        transform.localEulerAngles = new Vector3(smoothPitch, 0f, 0f);
    }

}
