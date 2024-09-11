using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
  // public static SceneTransitionController instance;
  public Animator animator;
  public Camera transitionCamera;

  public static readonly string SCENE_TRANSITION_NAME = "SceneTransition";

  public static string nextScene;
  public static string currentScene;

  public static TransitionType transitionType;

  public enum TransitionType
  {
    Door
  }

  private void Awake()
  {

    // if (instance != null)
    // {
    //   Debug.LogWarning("Multiples instances os SceneTransitionController");
    //   DestroyImmediate(this.gameObject);
    //   return;
    // }
    // instance = this;
    DontDestroyOnLoad(this);
  }
  void Start()
  {
    // instance.nextScene = nextScene;
    animator.SetTrigger("Door");
  }

  public void LoadNextScene()
  {
    Debug.Log("LoadNextScene");
    // SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
    SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
    Destroy(gameObject, 3);
  }

  public void UnloadLastScene()
  {
    Debug.Log("UnloadLastScene");
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.UnloadSceneAsync(currentScene);
  }

  public static void ToScene(string _nextScene, TransitionType type)
  {
    currentScene = SceneManager.GetActiveScene().name;
    nextScene = _nextScene;
    transitionType = type;
    SceneManager.LoadScene(SCENE_TRANSITION_NAME, LoadSceneMode.Additive);
  }

}
