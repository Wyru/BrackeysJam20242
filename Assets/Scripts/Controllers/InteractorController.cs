using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
  public LayerMask interactableLayer;
  private TMP_Text _possibleKeys;
  private IInteractable _interactObj;
  private IInteractableB _interactObjB;
  [SerializeField] private RawImage _interactionIcon;

  private Dictionary<string, string> interactionPrompts = new Dictionary<string, string>
    {
        { "PaperBag", "E to pickup" },
        { "Door", "E to Enter" },
        { "Computer", "E to Use" },
        { "CanPickUp", "E to interact" },
        { "Cat", "E to pet =^.^=" }
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

  private void Update()
  {
    CheckInteraction();

    // Check for interaction input using Input.GetKeyDown
    if (_interactObj != null && Input.GetKeyDown(KeyCode.E) && !DialogSystemController.Instance.isDialogRunning)
    {
      _interactObj.Interact();
    }

    // Check for remove item input using Input.GetKeyDown
    if (_interactObjB != null && Input.GetKeyDown(KeyCode.R) && !DialogSystemController.Instance.isDialogRunning)
    {
      _interactObjB.InteractB();
    }
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

        if (hitInfo.collider.gameObject.tag == "Cat")
        {
        }
        else if (interactionPrompts.TryGetValue(hitInfo.collider.gameObject.tag, out string prompt))
        {
          interactionMessage += "\nR to remove item";  // Add secondary interaction message
        }
        // You can have a separate dictionary for secondary interaction prompts

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
          ItemsController item = PickUpScript.instance.heldObj.GetComponent<ItemsController>();
          if (item != null && item.CanBeUsed)
          {
            interactionMessage += "\nQ to use";
          }
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

    if (DialogSystemController.Instance.isDialogRunning)
    {
      _possibleKeys.text = "E to skip dialogue";
    }
  }
}
