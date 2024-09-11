using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteractable
{

  public string sceneNameDestity;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }


  public void Interact()
  {
    Debug.Log(this);
    SceneTransitionController.ToScene(sceneNameDestity, SceneTransitionController.TransitionType.Door);
  }
}
