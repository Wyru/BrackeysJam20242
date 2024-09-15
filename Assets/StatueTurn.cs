using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueTurn : MonoBehaviour, IInteractable
{
    public enum StatueState
    {
        Turn0,
        Turn1,
        Turn2,
        Turn3
    }

    public Animator anim;
    public AudioSource clip;
    private StatueState state = StatueState.Turn0;
    public Transform spawn;
    public Transform spawnDirection;
    public bool isHitting = false;

    private void Update()
    {
        if (Physics.Raycast(spawn.transform.position, spawnDirection.position-spawn.position, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(spawn.transform.position, (spawnDirection.position-spawn.position)*100000000f, Color.green);
            if (hit.transform.gameObject.name == "CentralStatue")
            {
                Debug.Log(gameObject.transform.parent.name + " acertou a Central Statue");
                isHitting = true;
            }else{
                isHitting = false;
            }
        }
    }
    public void Interact()
    {
        switch (state)
        {
            case StatueState.Turn0:
                anim.SetBool("Turn0", true);
                anim.SetBool("Turn3", false);
                state = StatueState.Turn1;
                playSound();
                break;
            case StatueState.Turn1:
                anim.SetBool("Turn1", true);
                anim.SetBool("Turn0", false);
                state = StatueState.Turn2;
                playSound();
                break;
            case StatueState.Turn2:
                anim.SetBool("Turn2", true);
                anim.SetBool("Turn1", false);
                state = StatueState.Turn3;
                playSound();
                break;
            case StatueState.Turn3:
                anim.SetBool("Turn3", true);
                anim.SetBool("Turn2", false);
                state = StatueState.Turn0;
                playSound();
                break;
            default:
                break;
        }
    }


    public void playSound()
    {
        clip.PlayOneShot(clip.clip);
        StartCoroutine(WaitForClip());
    }

    IEnumerator WaitForClip()
    {
        while (clip.isPlaying)
        {
            yield return null;
        }
    }
}


