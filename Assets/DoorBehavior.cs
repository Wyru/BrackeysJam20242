using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteractable
{

  public string sceneNameDestity;

  public Transform thisSpawnPoint;

  public string destityDoorName;

  public SceneTransitionController.TransitionType type;

  public void Interact()
  {
    SceneTransitionController.ToScene(sceneNameDestity, type, destityDoorName);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject == PlayerController.instance.gameObject) { }
    Interact();
  }
}
