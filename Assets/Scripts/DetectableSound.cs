using UnityEngine;
using UnityEngine.Events;

public class DetectableSound : MonoBehaviour
{
  public UnityEvent OnCollideWithStatic;
  public GameObject soundSourcePrefab;
  public int detectionValue = 10;

  public bool canMakeSound = false;
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.isStatic && canMakeSound)
    {
      canMakeSound = false;
      var soundSource = Instantiate(soundSourcePrefab, transform.position, Quaternion.identity, null);
      soundSource.GetComponent<DetectableSoundSource>().Setup(detectionValue);
      OnCollideWithStatic?.Invoke();
    }
  }

  public void SetCanMakeSound(bool canMakeSound)
  {
    this.canMakeSound = canMakeSound;
  }

}
