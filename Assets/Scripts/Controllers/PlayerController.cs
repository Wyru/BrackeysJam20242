using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float _speed = 5f;

    [SerializeField]
    private Transform _cameraTransform;

    public Rigidbody _rb;
    private Vector2 _moveInput;
    public InputActionReference _inputs;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _moveInput = _inputs.action.ReadValue<Vector2>();
        Vector3 move = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        move.y = 0f;
        _rb.AddForce(move.normalized * _speed, ForceMode.VelocityChange);
    }

}
