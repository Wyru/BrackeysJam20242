using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorController : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    public InputActionReference _interact;

    void Start()
    {
        
    }

    private void Interacting(InputAction.CallbackContext context)
    {
        Debug.Log("Interacting");
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        _interact.action.started += Interacting;
    }

    private void OnDisable()
    {
        _interact.action.started -= Interacting;
    }


}
