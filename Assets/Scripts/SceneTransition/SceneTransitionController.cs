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

  public static string currentScene;
  public static SpawnPointScriptableObject nextSpawn;

  public static TransitionType transitionType;

  public enum TransitionType
  {
    Door, Street, Death
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

      case TransitionType.Death:
        animator.SetTrigger("Death");
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
    SceneManager.LoadScene(nextSpawn.targetSceneName);
  }

  public void UnloadLastScene()
  {
    Scene currentScene = SceneManager.GetActiveScene();
    SceneManager.UnloadSceneAsync(currentScene);
  }

  public static void ToScene(SpawnPointScriptableObject spawn, TransitionType type)
  {
    currentScene = SceneManager.GetActiveScene().name;
    nextSpawn = spawn;
    transitionType = type;
    PlayerController.instance.DisableMovement(true);
    SceneManager.LoadScene(SCENE_TRANSITION_NAME, LoadSceneMode.Additive);
  }

  public static void RestartScene(TransitionType type)
  {
    currentScene = SceneManager.GetActiveScene().name;
    transitionType = type;
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
    var points = FindObjectsOfType<SpawnPointBehavior>();
    foreach (var point in points)
    {
      if (point.spawnIdentifier == nextSpawn)
      {
        FindObjectOfType<PlayerController>()
        .transform.SetPositionAndRotation(
          point.transform.position,
          point.transform.rotation);

        Physics.SyncTransforms();
        return;
      }
    }

    Debug.LogError($"SpanwPointBevahior de {nextSpawn.name} não encontrado na cena {nextSpawn.targetSceneName}");
  }

}
