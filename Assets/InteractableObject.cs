using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable, IInteractableB
{
    public UnityEvent OnInteract;
    public UnityEvent OnInteractB;

    public void Interact()
    {
        OnInteract?.Invoke();
    }

    public void InteractB()
    {
        OnInteractB?.Invoke();
    }

}
