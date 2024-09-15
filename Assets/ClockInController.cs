using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockInInteraction : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        OfficeManager.instance.PlayerClockIn();
    }

}
