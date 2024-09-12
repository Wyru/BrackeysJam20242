using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
  // public static SceneTransitionController instance;
  public Animator animator;
  public Camera transitionCamera;

  public Transform playerPos;

  public AudioSource doorAudioSource;
  public AudioSource stepsAudioSource;

  public static readonly string SCENE_TRANSITION_NAME = "SceneTransition";

  public static string nextScene;
  public static string currentScene;
  public static string doorName;

  public static TransitionType transitionType;

  public enum TransitionType
  {
    Door, Street
  }

  private void Awake()
  {
    DontDestroyOnLoad(this);
  }
  void Start()
  {
    switch (transitionType)
    {
      case TransitionType.Door:
        animator.SetTrigger("Door");
        doorAudioSource.Play();
        break;

      case TransitionType.Street:
        animator.SetTrigger("Street");
        stepsAudioSource.Play();
        break;

      default:
        animator.SetTrigger("Door");
        doorAudioSource.Play();
        break;
    }
  }

  public void LoadNextScene()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
    SceneManager.LoadScene(nextScene);
  }

  public void UnloadLastScene()
  {
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.UnloadSceneAsync(currentScene);
  }

  public static void ToScene(string _nextScene, TransitionType type, string _doorName)
  {
    currentScene = SceneManager.GetActiveScene().name;
    nextScene = _nextScene;
    transitionType = type;
    doorName = _doorName;
    PlayerController.instance.DisableMovement(true);
    SceneManager.LoadScene(SCENE_TRANSITION_NAME, LoadSceneMode.Additive);
  }

  public void HidePlayer()
  {
    PlayerController.instance.transform.position = playerPos.position;
  }


  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    MovePlayerToDoor();
    PlayerController.instance.DisableMovement(false);
    Destroy(gameObject, 2);
  }

  private void OnDestroy()
  {
    SceneManager.sceneLoaded -= OnSceneLoaded;
  }

  void MovePlayerToDoor()
  {
    var doors = FindObjectsOfType<DoorBehavior>();
    foreach (var door in doors)
    {
      if (door.name == doorName)
      {
        FindObjectOfType<PlayerController>()
        .transform.SetPositionAndRotation(
          door.thisSpawnPoint.position,
          door.thisSpawnPoint.rotation);

        Physics.SyncTransforms();
      }
    }
  }

}
