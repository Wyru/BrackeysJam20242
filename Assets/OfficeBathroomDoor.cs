using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeBathroomDoor : MonoBehaviour, IInteractable
{
    bool doorStatus = false;
    public Animator anim;
    List<string> lista = new List<string> { "You smell a putrid aroma coming out of the bathroom.", "Regardless, you decide to open the door." };

    public void Interact()
    {
        if (doorStatus)
        {
            doorStatus = false;
            anim.Play("closeani");
        }
        else
        {
            DialogSystemController.ShowDialogs(lista);
            StartCoroutine(Wait());
        }

    }

    IEnumerator Wait()
    {
        while (DialogSystemController.Instance.isDialogRunning)
        {
            yield return null;
        }
            anim.Play("Door");
        doorStatus = true;
        transform.Rotate(new Vector3(transform.rotation.x, 90, transform.rotation.z));
    }

}
