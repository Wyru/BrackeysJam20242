using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class EquipableObjects : MonoBehaviour, IInteractable
{
    [SerializeField]
    public int attackDamage;
    [SerializeField]
    public int durabilityDropRate;
    [SerializeField]
    public int durability = 100;
    public UnityEvent onBreakEvent;
    public ParticleSystem breakParticle;

    public void Interact()
    {
        PlayerController.instance.EquipWeapon(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Actor>().TakeDamage(attackDamage);
            PlayerController.instance.PlaySound();
        }
    }

    public void onBreak()
    {
        //break mesh
        Debug.Log(gameObject.name + " broke!!");
        StartCoroutine(DisplayBreakMessage("Your " + gameObject.name + " is broken"));
        breakParticle.Play();
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    IEnumerator DisplayBreakMessage(string message)
    {
        DefaultCanvasBehavior.instance.itemNotifications.text = message;
        yield return new WaitForSeconds(1.5f);
        DefaultCanvasBehavior.instance.itemNotifications.text = "";
        Destroy(gameObject);
    }
}
