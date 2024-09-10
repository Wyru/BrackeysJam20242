using UnityEngine;

public class Timer : MonoBehaviour
{
  public float seconds;

  [SerializeField]
  float secondsCounter;

  [SerializeField]
  bool timeout = false;


  public bool Timeout
  {
    get { return timeout; }
  }

  void Start()
  {
    secondsCounter = -1;
  }

  // Update is called once per frame
  void Update()
  {
    if (secondsCounter < 0)
    {
      timeout = true;
      return;
    }

    secondsCounter -= Time.deltaTime;
  }

  public void StartTimer()
  {
    timeout = false;
    secondsCounter = seconds;
  }
}
