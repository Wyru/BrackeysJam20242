using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPuzzle : MonoBehaviour
{

    public StatueTurn statue1;
    public StatueTurn statue2;
    public StatueTurn statue3;
    public Animator anim;
    public Light lights;
    public AudioSource clip;
    private bool open = false;

    // Update is called once per frame
    void Update()
    {
        if (statue1.isHitting && statue2.isHitting && statue3.isHitting && !open)
        {
            anim.SetBool("canOpen", true);
            clip.PlayOneShot(clip.clip);
            lights.enabled = true;
            open = true;
        }
    }
}
