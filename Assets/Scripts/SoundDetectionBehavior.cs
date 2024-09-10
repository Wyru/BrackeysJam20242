using System;
using UnityEngine;

public class SoundDetectionBehavior : MonoBehaviour
{
  public Action<Vector3, int> OnHearObject;
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 9)
    {

      Debug.Log("Sound detected!");

      if (other.TryGetComponent<DetectableSoundSource>(out var sound))
        OnHearObject?.Invoke(other.transform.position, sound.soundVolume);
    }
  }
}
