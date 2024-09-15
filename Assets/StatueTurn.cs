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


    public void playSound(){
        clip.PlayOneShot(clip.clip);
        StartCoroutine(WaitForClip());
    }

    IEnumerator WaitForClip(){
        while (clip.isPlaying){
            yield return null;
        }
    }
}


