using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float _sensitivity;

    private Vector2 _mouseInput;

    private float _pitch;
    public InputActionReference _inputs;
    public Transform playerBody;
    private float smoothPitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        // Read mouse input
        _mouseInput = _inputs.action.ReadValue<Vector2>();

        // Rotate the player body along the Y-axis (horizontal movement)
        playerBody.Rotate(Vector3.up, _mouseInput.x * _sensitivity * Time.deltaTime);
    }

    void LateUpdate()
    {
        // Handle the pitch (up and down camera movement)
        _pitch -= _mouseInput.y * _sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);

        // Smooth the pitch using Lerp
        smoothPitch = Mathf.Lerp(smoothPitch, _pitch, 0.5f);

        // Apply smoothed pitch to the camera
        transform.localEulerAngles = new Vector3(smoothPitch, 0f, 0f);
    }
}
