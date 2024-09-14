using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
  public float seconds;

  [SerializeField]
  public float secondsCounter;

  [SerializeField]
  bool timeout = false;

  public Action OnTimerEnd;

  public bool Timeout
  {
    get { return timeout; }
  }

  bool loop = false;

  void Start()
  {
    secondsCounter = -1;
  }

  // Update is called once per frame
  void Update()
  {
    if (timeout)
      return;

    secondsCounter -= Time.deltaTime;

    if (secondsCounter < 0)
    {
      if (!timeout)
      {
        OnTimerEnd?.Invoke();
      }

      timeout = true;

      if (loop)
      {
        timeout = false;
        secondsCounter = seconds;
      }
    }
  }

  public void StartTimer(bool loop = false)
  {
    timeout = false;
    this.loop = loop;
    secondsCounter = seconds;
  }
}
