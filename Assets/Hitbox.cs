using System;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Action<Collider> OnTriggerEnterEvent;
    public Action<Collider> OnTriggerStayEvent;
    public Action<Collider> OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayEvent?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }
}
