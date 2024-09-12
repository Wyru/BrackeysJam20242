using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteractable
{

  public string sceneNameDestity;

  public Transform thisSpawnPoint;

  public string destityDoorName;

  public void Interact()
  {
    SceneTransitionController.ToScene(sceneNameDestity, SceneTransitionController.TransitionType.Door, destityDoorName);
  }
}
