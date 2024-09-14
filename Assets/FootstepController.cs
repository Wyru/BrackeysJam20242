using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepController : MonoBehaviour
{
  // Sons dos passos
  public AudioClip footstepSound1;
  public AudioClip footstepSound2;

  // Referência para o AudioSource
  public AudioSource audioSource;

  // Configurações para variar o pitch
  public float minPitch = 0.9f;
  public float maxPitch = 1.1f;

  // Intervalo entre os sons dos passos
  public float stepInterval = 0.5f;
  public float runStepInterval = 0.5f;

  private float nextStepTime;

  public bool isWalking;
  public bool isRunning;

  private void Start()
  {
    if (audioSource == null)
    {
      audioSource = GetComponent<AudioSource>();
    }

    nextStepTime = 0f;
  }

  private void Update()
  {
    if (isWalking && Time.time >= nextStepTime)
    {
      PlayFootstepSound();
      nextStepTime = Time.time + (isRunning ? runStepInterval : stepInterval);
    }
  }

  void PlayFootstepSound()
  {
    AudioClip selectedClip = Random.value > 0.5f ? footstepSound1 : footstepSound2;
    audioSource.pitch = Random.Range(minPitch, maxPitch);
    audioSource.PlayOneShot(selectedClip);
  }
}
