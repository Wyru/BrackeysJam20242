using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelStatueDialogue : MonoBehaviour, IInteractable
{
    List<string> lista = new List<string> { "The angel watches in silence, her eyes unseeing yet ever aware. When others gaze upon her, their attention fixed, the path will reveal itself.", "A cryptic message. You wonder what it means." };

    bool canInteract = true;

    public void Interact()
    {
        if (!canInteract)
            return;

        DialogSystemController.ShowDialogs(lista, () =>
          {
              Invoke(nameof(ResetDialog), 1);
          });
        canInteract = false;
    }


    void ResetDialog()
    {
        Debug.Log("callback chamado!");
        canInteract = true;
    }
}
