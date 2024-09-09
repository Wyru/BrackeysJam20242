using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class NumberGenerator : MonoBehaviour, IInteractable
{
    public void Interact(){
        Debug.Log(Random.Range(0,100));
    }
}
