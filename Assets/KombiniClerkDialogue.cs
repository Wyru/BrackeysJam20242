using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KombiniClerkDialogue : MonoBehaviour, IInteractable
{
    public List<string> frases;
    public void Interact()
    {
        DialogSystemController.ShowDialogs(frases);
    }
}
