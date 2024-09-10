using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    public float _speed = 5f;
    Vector3 _PlayerVelocity;


    [SerializeField]
    private Transform _cameraTransform;

    [SerializeField]
    private Transform _weaponSlot;
    private GameObject _equipedWeapon;

    public Rigidbody _rb;
    private bool isGrounded;
    CharacterController controller;
    Animator animator;
    AudioSource audioSource;
    
    private Camera cam;

    [Header("WeaponAttributes")]
    public bool weaponEquipped;
    public float throwforce;

    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;
        if(instance == null){
            instance = this;
        }
    }

    public void DisableController()
    {
        InputManager.instance.OnDisable();
        CameraController.instance.locked = true;
        
    }

    public void EnableController()
    {
        controller.enabled = true;
        animator.enabled = true;
        InputManager.instance.enabled = true;
        InteractorController.instance.enabled = true;
        this.enabled = true;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SetAnimations();
    }
    
    public void ProcessMove(Vector2 _input)
    {
        Vector3 move = _cameraTransform.forward * _input.y + _cameraTransform.right * _input.x;
        move.y = 0f;
        _rb.AddForce(move.normalized * _speed, ForceMode.VelocityChange);

    }

    public void ProcessAttack(bool _attack)
    {
        if(_attack){ Attack(true); }
    }

    // ---------- //
    // ANIMATIONS //
    // ---------- //

    public const string SwordIdle = "Sword_Idle";
    public const string FistIdle = "Fist_Idle";
    public string IDLE
    {
        get
        {
            return weaponEquipped ? SwordIdle : FistIdle;
        }
    }
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";
    public const string ATTACK3 = "Pull_Animation";
    public const string THROW = "Throw_Animation";

    string currentAnimationState;

    public void ChangeAnimationState(string newState) 
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {

        // If player is not attacking
        if(!attacking)
        {
            if(_PlayerVelocity.x == 0 &&_PlayerVelocity.z == 0)
            { 
                ChangeAnimationState(IDLE); 
            }
            else
            { ChangeAnimationState(WALK); }
        }
    }

    // ------------------- //
    // ATTACKING BEHAVIOUR //
    // ------------------- //

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    private int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    public void Attack(bool melee)
    {
        if(!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);
        if(melee){       
            if(weaponEquipped){
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(swordSwing);

                if(attackCount == 0)
                {
                    ChangeAnimationState(ATTACK1);
                    attackCount++;
                }
                else
                {
                    ChangeAnimationState(ATTACK2);
                    attackCount = 0;
                }
            }else{
                audioSource.pitch = Random.Range(2f, 2.5f);
                audioSource.PlayOneShot(swordSwing);

                ChangeAnimationState(ATTACK3);
                attackCount = 0;
            }
        }else{
            if(weaponEquipped){

                ChangeAnimationState(THROW);
                attackCount = 0;
            }
        }
        

        
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        { 
            HitTarget(hit.point);

            if(hit.transform.TryGetComponent<Actor>(out Actor T))
            { T.TakeDamage(attackDamage); }
        } 
    }

    void HitTarget(Vector3 pos)
    {
        if(weaponEquipped){
            audioSource.pitch = 1;
            audioSource.PlayOneShot(hitSound);

            GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
            Destroy(GO, 20);
        }else{
            audioSource.pitch = 0.5f;
            audioSource.PlayOneShot(hitSound);

            GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
            Destroy(GO, 20);
        }
    }

    public void PlaySound()
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);
    }

    public void EquipWeapon(GameObject equippable)
    {
        if(_equipedWeapon == null){
            attackDamage = equippable.GetComponent<EquipableObjects>().attackDamage;
            equippable.GetComponent<Rigidbody>().isKinematic = true;
            equippable.GetComponent<Rigidbody>().detectCollisions = false;
            equippable.transform.SetParent(_weaponSlot.transform);
            equippable.transform.localPosition = Vector3.zero;
            equippable.transform.rotation = _weaponSlot.rotation;
            _equipedWeapon = equippable;
            weaponEquipped = true;
        }
    }

    public void DropWeapon(bool _drop)
    {
        if(_drop)
        {
            if(_equipedWeapon != null){
                _equipedWeapon.GetComponent<Rigidbody>().isKinematic = false;
                _equipedWeapon.GetComponent<Rigidbody>().detectCollisions = true;
                _equipedWeapon.transform.SetParent(null);
                _equipedWeapon = null;
                weaponEquipped = false;
            }
        }
    }

    public void Throwing(bool _throw)
    {
        if(_throw)
        {
            { Attack(false); }
        }
    }

    public void ThrowWeapon(){
         if(_equipedWeapon != null){

            Rigidbody equippedRb = _equipedWeapon.GetComponent<Rigidbody>();

            _equipedWeapon.GetComponent<Rigidbody>().detectCollisions = true;
            _equipedWeapon.layer = 0;
            equippedRb.isKinematic = false;
            _equipedWeapon.transform.parent = null;
            equippedRb.AddForce(transform.forward * throwforce);
            _equipedWeapon = null;
            weaponEquipped = false;
        }
    }

}
