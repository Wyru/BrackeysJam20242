using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Animator animator;
    public Image lifeDamageTexture;

    public AudioSource lowLife;
    public AudioSource lifeItemsAudioSource;

    public AudioClip healSound;

    public float lowLifeSoundThreshhold = .3f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        GameManager.OnHealthChange += UpdateUI;
    }

    private void OnDisable()
    {
        GameManager.OnHealthChange -= UpdateUI;

    }

    public void UpdateUI(int change, int currentHealth, int maxHealth)
    {
        Color x = lifeDamageTexture.color;
        x.a = 1 - ((float)currentHealth / maxHealth);
        lifeDamageTexture.color = x;
        lowLife.enabled = (float)currentHealth / maxHealth < lowLifeSoundThreshhold;

        if (change < 0)
            animator.SetTrigger("Show");
        else
        {
            animator.SetTrigger("Heal");
            lifeItemsAudioSource.PlayOneShot(healSound);
        }
    }

    public void PlayHealSound()
    {
        lifeItemsAudioSource.PlayOneShot(healSound);
    }
}
