using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteractable
{

  public string sceneNameDestity;

  public Transform thisSpawnPoint;

  public string destityDoorName;

  public SceneTransitionController.TransitionType type;

  bool activated = false;
  public void Interact()
  {
    if (!activated)
    { // impede que o player interaja duas vezes seguidas
      activated = true;
      SceneTransitionController.ToScene(sceneNameDestity, type, destityDoorName);
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
