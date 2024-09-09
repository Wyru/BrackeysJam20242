using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity;

    private Vector2 _mouseInput;

    private float _pitch;
    public InputActionReference _inputs;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;   
    }

    void Update()
    {
        _mouseInput = _inputs.action.ReadValue<Vector2>();
        Debug.Log(_mouseInput);

        transform.Rotate(Vector3.up, _mouseInput.x * _sensitivity * Time.deltaTime);

        _pitch -= _mouseInput.y * _sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        transform.localEulerAngles = new Vector3(_pitch, transform.localEulerAngles.y, 0f);
    }
}
