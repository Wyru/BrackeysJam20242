using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathController : MonoBehaviour
{

  public AudioSource audioSource;
  public float breathTrhreslhld = .4f;
  public bool fatigue = false;

  void Start()
  {
    GameManager.OnStaminaChange += OnStaminaChange;
  }

  public void OnStaminaChange(float value, float current, int max)
  {


    if (fatigue)
    {
      if (current / max > breathTrhreslhld)
      {
        audioSource.loop = false;
        fatigue = false;
      }
      return;
    }


    if (current / max < breathTrhreslhld)
    {
      if (!audioSource.isPlaying)
      {
        audioSource.loop = true;

        audioSource.Play();
      }
      return;
    }

    audioSource.loop = false;
  }

  private void OnDisable()
  {
    GameManager.OnStaminaChange -= OnStaminaChange;
  }
}
