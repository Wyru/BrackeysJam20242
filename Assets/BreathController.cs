using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathController : MonoBehaviour
{

    public AudioSource audioSource;
    public float breathTrhreslhld = .4f;

    public AnimationCurve breathSoundVolumeCurve;

    private void Update()
    {
        PlayBreathSound();
    }

    void PlayBreathSound()
    {
        audioSource.volume = breathSoundVolumeCurve.Evaluate(PlayerBehavior.Fatigue.StaminaPercentage);

        if (PlayerBehavior.Fatigue.isFatigued)
        {
            if (PlayerBehavior.Fatigue.StaminaPercentage > breathTrhreslhld)
            {
                audioSource.loop = false;
            }

            return;
        }

        if (PlayerBehavior.Fatigue.StaminaPercentage < breathTrhreslhld)
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
}
