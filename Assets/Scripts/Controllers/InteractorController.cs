using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

interface IInteractable
{
    public void Interact();
}

public class InteractorController : MonoBehaviour
{
    public static InteractorController instance;
    public Transform InteractorSource;
    public float InteractRange;
    public InputActionReference _interact;

    public LayerMask interactableLayer;

    private IInteractable _interactObj;
    [SerializeField] private RawImage _interactionIcon;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        var defaultCanvasBehavior = FindAnyObjectByType<DefaultCanvasBehavior>();
        if (defaultCanvasBehavior != null)
        {
            _interactionIcon = defaultCanvasBehavior.hand;
        }
    }

    private void Interacting(InputAction.CallbackContext context)
    {
        if (_interactObj != null) _interactObj.Interact();

    }

    private void Update()
    {
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, interactableLayer))
        {

            Debug.Log(hitInfo.collider.gameObject.name);

            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {

                Debug.Log(hitInfo.collider.gameObject.name);


                _interactObj = interactObj;
                _interactionIcon.enabled = true;
            }
            else
            {
                _interactObj = null;
                _interactionIcon.enabled = false;
            }
        }
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
