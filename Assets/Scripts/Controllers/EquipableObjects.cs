using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class EquipableObjects : MonoBehaviour, IInteractable
{
    public Vector3 equipedPosition;
    public Vector3 equipedRotation;
    public int attackDamage;
    public int durabilityDropRate;
    public int durability = 100;
    public UnityEvent onBreakEvent;
    public ParticleSystem breakParticle;

    public Rigidbody rb;

    int layer;

    public enum WeaponType
    {
        Melee,
        Gun
    }

    public WeaponType type;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        layer = gameObject.layer;
    }

    public void Interact()
    {
        // PlayerController.instance.EquipWeapon(this.gameObject);
        PlayerBehavior.instance.playerWeapon.Equip(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Actor>().TakeDamage(attackDamage);
            PlayerController.instance.PlaySound();
        }
    }

    public void OnBreak()
    {
        //break mesh
        Debug.Log(gameObject.name + " broke!!");
        PlayerBehavior.instance.playerWeapon.DropWeapon();
        StartCoroutine(DisplayBreakMessage("Your " + gameObject.name + " is broken"));
        breakParticle.Play();
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void OnEquip(int layer)
    {
        rb.isKinematic = true;
        transform.SetLocalPositionAndRotation(equipedPosition, Quaternion.Euler(equipedRotation));
        gameObject.layer = layer;
    }

    public void OnDrop()
    {
        rb.isKinematic = false;
        gameObject.layer = layer;
        transform.SetParent(null);
    }

    public void DropDurability()
    {
        durability -= durabilityDropRate;
        if (durability <= 0)
        {
            OnBreak();
        }
    }

    IEnumerator DisplayBreakMessage(string message)
    {
        DefaultCanvasBehavior.instance.itemNotifications.text = message;
        yield return new WaitForSeconds(1.5f);
        DefaultCanvasBehavior.instance.itemNotifications.text = "";
        Destroy(gameObject);
    }
}
