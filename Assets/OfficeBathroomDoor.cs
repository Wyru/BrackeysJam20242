using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeBathroomDoor : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        DialogSystemController.ShowDialogs();
    }

}
