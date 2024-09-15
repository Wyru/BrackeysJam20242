using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteractable
{
  public SpawnPointScriptableObject destinySpawnPoint;
  public SceneTransitionController.TransitionType type;


  public bool advanceDay = false;

  bool activated = false;
  public void Interact()
  {
    if (!activated)
    { // impede que o player interaja duas vezes seguidas
      activated = true;
      GameManager.instance.IncrementDay();
      if (GameManager.instance.day > 3)
      {
        DialogSystemController.ShowDialogs(new List<string> {
          "You survived time! And now, it's up to you to make up time for yourself, until the next storm there's still calm to be had, until the clock strikes...",
        });
      }
      SceneTransitionController.ToScene(destinySpawnPoint, type);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject == PlayerController.instance.gameObject && !activated)
    {
      Interact();
      // player está colidindo duas vezes por algum motivo
      activated = true;
    }
  }
}
