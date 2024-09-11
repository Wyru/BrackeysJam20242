using UnityEngine;

public class DetectableSoundSource : MonoBehaviour
{
  public int soundVolume = 0;
  public Timer timer;

  bool ready = false;

  public void Setup(int volume)
  {
    soundVolume = volume;
    timer.StartTimer();
    ready = true;
  }

  private void Update()
  {
    if (timer.Timeout && ready)
    {
      Destroy(gameObject);
    }
  }

}
