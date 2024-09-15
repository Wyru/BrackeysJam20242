using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KombiniClerkDialogue : MonoBehaviour, IInteractable
{
    public List<string> frases;

    public bool canInteract;
    public void Interact()
    {
        if (!canInteract)
            return;

        DialogSystemController.ShowDialogs(
          frases,
          () =>
          {
              Invoke(nameof(ResetDialog), 1);
          }
        );
        canInteract = false;
    }

    void ResetDialog()
    {
        Debug.Log("callback chamado!");
        canInteract = true;
    }
}
