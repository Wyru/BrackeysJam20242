using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

interface IInteractable
{
    public void Interact();
}

interface IInteractableB
{
    public void InteractB();
}

public class InteractorController : MonoBehaviour
{
    public static InteractorController instance;
    public Transform InteractorSource;
    public float InteractRange;
    public InputActionReference _interact;
    public InputActionReference _interactRemoveItem;
    public LayerMask interactableLayer;
    private TMP_Text _possibleKeys;
    private IInteractable _interactObj;
    private IInteractableB _interactObjB;
    [SerializeField] private RawImage _interactionIcon;

    private Dictionary<string, string> interactionPrompts = new Dictionary<string, string>
    {
        { "PaperBag", "E to pickup" },
        { "Door", "E to Enter" },
        { "Computer", "E to Use"},
        { "CanPickUp", "E to interact"}
    };

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
            _possibleKeys = defaultCanvasBehavior.possibleKeys;
        }
    }

    private void Interacting(InputAction.CallbackContext context)
    {
        if (_interactObj != null) _interactObj.Interact();
    }
    private void InteractingRemoveItem(InputAction.CallbackContext context)
    {
        if (_interactObjB != null) _interactObjB.InteractB();
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
            bool hasInteractable = false;
            string interactionMessage = "";  // To store the prompt message

            // Check if the object has an IInteractable component
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                _interactObj = interactObj;
                hasInteractable = true;

                // Retrieve the prompt based on the object's tag or type
                if (interactionPrompts.TryGetValue(hitInfo.collider.gameObject.tag, out string prompt))
                {
                    interactionMessage = prompt;
                }
                else
                {
                    interactionMessage = "Press E to interact";  // Default message if no specific prompt is found
                }
            }
            else
            {
                _interactObj = null;
            }

            // Check if the object has an IInteractableB component
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractableB interactObjB))
            {
                _interactObjB = interactObjB;
                hasInteractable = true;

                // You can have a separate dictionary for secondary interaction prompts
                if (interactionPrompts.TryGetValue(hitInfo.collider.gameObject.tag, out string prompt))
                {
                    interactionMessage += "\nR to remove item";  // Add secondary interaction message
                }
            }
            else
            {
                _interactObjB = null;
            }

            // Show interaction text if an interactable is found
            if (hasInteractable)
            {
                _interactionIcon.enabled = true;
                _possibleKeys.enabled = true;
                _possibleKeys.text = interactionMessage;  // Display the interaction message
            }
            else if (hitInfo.collider.gameObject.CompareTag("CanPickUp"))
            {
                if (PickUpScript.instance.heldObj == null)
                {
                    if (interactionPrompts.TryGetValue(hitInfo.collider.gameObject.tag, out string prompt))
                    {
                        interactionMessage = prompt;
                    }
                }
                else
                {
                    interactionMessage = "E to drop\nR to rotate\nRMouse to throw";
                }

                _interactionIcon.enabled = true;
                _possibleKeys.enabled = true;
                _possibleKeys.text = interactionMessage;
            }
            else
            {
                _interactionIcon.enabled = false;
                _possibleKeys.enabled = false;
            }
        }
        else
        {
            _interactObj = null;
            _interactObjB = null;
            _interactionIcon.enabled = false;
            _possibleKeys.enabled = false;
        }
    }

    private void OnEnable()
    {
        _interact.action.performed += Interacting;
        _interactRemoveItem.action.performed += InteractingRemoveItem;
    }

    private void OnDisable()
    {
        _interact.action.performed -= Interacting;
        _interactRemoveItem.action.performed -= InteractingRemoveItem;
    }
}
