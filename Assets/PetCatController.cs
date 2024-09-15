using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCatController : MonoBehaviour
{


  public AudioSource audioSource;
  public AudioClip[] audioClips;

  int x = 0;
  public void Pet()
  {
    PlayerController.instance.animator.SetTrigger("pet");

    audioSource.PlayOneShot(audioClips[x]);

    x = (x + 1) % audioClips.Length;

  }
}
