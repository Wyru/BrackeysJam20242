using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AttackSoundBehavior : MonoBehaviour
{
    public AudioClip attackSound;
    public AudioClip weaponAttackSound;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        PlayerWeaponBehavior.OnAttackEvent += PlayAttackSound;
        PlayerWeaponBehavior.OnThrowWeaponEvent += PlayeThrowSound;
    }

    private void OnDisable()
    {
        PlayerWeaponBehavior.OnAttackEvent -= PlayAttackSound;
        PlayerWeaponBehavior.OnThrowWeaponEvent -= PlayeThrowSound;

    }

    public void PlayeThrowSound()
    {
        ChangePitch();

        audioSource.PlayOneShot(weaponAttackSound);
    }

    public void PlayAttackSound(EquipableObjects weapon)
    {
        ChangePitch();

        if (weapon == null)
        {
            audioSource.PlayOneShot(attackSound);

            return;
        }

        audioSource.PlayOneShot(weaponAttackSound);
    }

    void ChangePitch()
    {
        audioSource.pitch = Random.Range(.9f, 1.1f);
    }
}
