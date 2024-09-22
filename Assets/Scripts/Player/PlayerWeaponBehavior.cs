using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponBehavior : MonoBehaviour
{
    readonly string ATTACK_ANI_TRIGGER = "punch";
    readonly string WEAPON_MELEE_ATTACK_ANI_TRIGGER = "weaponSlash";

    public string equippedWeaponLayerName = "Hold"; // Nome da camada
    private int equippedWeaponLayer; // Valor da camada
    public Transform weaponPositionPivot;
    public EquipableObjects currentWeapon;
    public Hitbox hitbox;

    [Header("Events")]
    public static Action<EquipableObjects> OnAttackEvent;
    public static Action OnHitEvent;
    public static Action OnThrowWeaponEvent;
    public static Action OnDropEvent;


    void Start()
    {
        equippedWeaponLayer = LayerMask.NameToLayer(equippedWeaponLayerName);

        if (equippedWeaponLayer == -1)
        {
            Debug.LogError($"A camada '{equippedWeaponLayerName}' não existe! Certifique-se de que está configurada nas camadas do projeto.");
        }

        hitbox.OnTriggerEnterEvent += DealDamage;
    }

    void Update()
    {
        PlayerBehavior.ArmsAni.SetBool("weapon", currentWeapon != null);

        if (currentWeapon == null)
            return;

        if (PlayerBehavior.instance.dropWeaponInput.action.WasPerformedThisFrame())
        {
            DropWeapon();
        }

    }

    public void Attack()
    {
        PlayerBehavior.instance.armsAnimator.SetTrigger("attack");
        OnAttackEvent?.Invoke(currentWeapon);
    }

    public void Equip(EquipableObjects weapon)
    {
        if (currentWeapon != null)
            DropWeapon();

        weapon.transform.SetParent(weaponPositionPivot.transform);
        weapon.OnEquip(equippedWeaponLayer);
        currentWeapon = weapon;
    }

    public void Throw()
    {
        if (currentWeapon == null)
            return;

        var weapon = currentWeapon;

        DropWeapon();

        var foward = PlayerBehavior.instance.playerCameraBehavior.playerCamera.transform.forward;

        weapon.rb.AddForce(
            foward * 10,
            ForceMode.Impulse
        );

        //weapon.DropDurability();
        OnThrowWeaponEvent?.Invoke();

    }

    public void DropWeapon()
    {
        if (currentWeapon == null)
            return;

        currentWeapon.OnDrop();
        currentWeapon = null;
        OnDropEvent?.Invoke();
    }

    public void DealDamage(Collider other)
    {
        if (other.TryGetComponent<Actor>(out var actor))
        {
            actor.TakeDamage(currentWeapon?.attackDamage ?? 1);
            OnHitEvent?.Invoke();
        }
    }

}
