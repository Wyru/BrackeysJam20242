using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelStatueDialogue : MonoBehaviour, IInteractable
{
    List<string> lista = new List<string> {"The angel watches in silence, her eyes unseeing yet ever aware. When others gaze upon her, their attention fixed, the path will reveal itself.", "A cryptic message. You wonder what it means."};

    public void Interact()
    {
        DialogSystemController.ShowDialogs(lista);
    }
}
