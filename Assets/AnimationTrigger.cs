using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTrigger : MonoBehaviour
{
    public UnityEvent events;

    public void TriggerEvent()
    {
        events.Invoke();
    }
    
}
