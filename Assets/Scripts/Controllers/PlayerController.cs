using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  public static PlayerController instance;

  [SerializeField]

  public float speed = 5;
  public float runningSpeed = 5;
  public float fatigueSpeed = 3;

  Vector3 _PlayerVelocity;


  [SerializeField]
  private Transform _cameraTransform;

  [SerializeField]
  private Transform _weaponSlot;
  private GameObject _equipedWeapon;

  public Rigidbody _rb;
  private bool isGrounded;
  CharacterController controller;
  public Animator animator;
  AudioSource audioSource;

  private Camera cam;

  public Camera MyCamera
  {
    get
    {

      if (cam == null)
      {
        cam = Camera.main;
      }

      return cam;

    }
  }

  [Header("WeaponAttributes")]
  public bool weaponEquipped;
  public float throwforce;

  public DefaultCanvasBehavior canvas;

  bool _disableMovement = false;

  public FootstepController footstepController;

  GameManager gameManager;


  [Header("Stamina")]

  public float startRunStaminaCost = 10f;
  public float staminaRecoveryRate = 20f;
  public float staminaDropRate = 20f;
  public float attackStaminaCost = 15f;
  public float attackStaminaCostWeapon = 25f;
  public float recoveryFatigueThreshhold = .4f;
  public bool fatigue = false;

  bool wasRunning = true;

  public PickUpScript pickUpScript;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Debug.LogWarning("Multiplas instâncias do player!");
      DestroyImmediate(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);

    controller = GetComponent<CharacterController>();
    animator = GetComponentInChildren<Animator>();
    audioSource = GetComponent<AudioSource>();
    cam = Camera.main;
    gameManager = FindAnyObjectByType<GameManager>();
    canvas = FindAnyObjectByType<DefaultCanvasBehavior>();

  }

  public void DisableController()
  {
    InputManager.instance.OnDisable();
    CameraController.instance.locked = true;

  }

  public void EnableController()
  {
    // controller.enabled = true;
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


    if (!wasRunning)
    {
      GameManager.instance.SetStamina(staminaRecoveryRate * Time.deltaTime);
    }

    if (GameManager.instance.stamina / GameManager.instance.maxStamina > recoveryFatigueThreshhold)
    {
      fatigue = false;
    }

    if (DialogSystemController.Instance.isDialogRunning)
    {

      animator.SetBool(WALKING, false);
      animator.SetBool(RUNNING, false);
      animator.SetBool("fatigue", false);
      _rb.velocity = Vector3.zero;
      return;
    }

    if (_equipedWeapon != null)
    {
      animator.SetBool("flashlight", _equipedWeapon.name.Contains("Flashlight"));
    }
    else
    {
      animator.SetBool("flashlight", false);
    }

    // if (Input.GetKeyDown(KeyCode.Escape))
    // {
    //   OpenMenu();
    // }
  }

  public void ProcessMove(Vector2 _input)
  {
    if (_disableMovement || DialogSystemController.Instance.isDialogRunning)
      return;

    Vector3 move = transform.forward * _input.y + transform.right * _input.x;

    footstepController.isWalking = move.magnitude != 0;
    footstepController.isFatigue = fatigue;


    if (move.magnitude == 0)
    {
      animator.SetBool(WALKING, false);
      animator.SetBool(RUNNING, false);
      return;
    }

    animator.SetBool(WALKING, true);
    animator.SetBool("fatigue", fatigue);

    bool inputRunning = Input.GetKey(KeyCode.LeftShift);

    if (inputRunning && wasRunning == false)
    {
      // on start running
      GameManager.instance.SetStamina(-startRunStaminaCost);
    }

    if (GameManager.instance.stamina < 3)
      fatigue = true;

    if (inputRunning)
      GameManager.instance.SetStamina(-staminaDropRate * Time.deltaTime);

    wasRunning = inputRunning;

    animator.SetBool(RUNNING, inputRunning);

    footstepController.isRunning = inputRunning;

    if (fatigue)
      _rb.velocity = move * fatigueSpeed;
    else
      _rb.velocity = move * (inputRunning ? runningSpeed : speed);

  }


  public void ProcessAttack(bool _attack)
  {
    if (_attack) { Attack(true); }
  }

  // ---------- //
  // ANIMATIONS //
  // ---------- //


  public const string WALKING = "walking";
  public const string RUNNING = "running";
  public const string WEAPON_SLASH_TRIGGER = "weaponSlash";
  public const string PUSH_ENEMY_TRIGGER = "pushEnemy";
  public const string PUNCH_ENEMY_TRIGGER = "punch";
  public const string THROW_TRIGGER = "throw";

  // ------------------- //
  // ATTACKING BEHAVIOUR //
  // ------------------- //

  [Header("Attacking")]
  public AttackCollider attackHitbox;
  public float attackDelay = 0.4f;
  public float attackSpeed = 1f;
  public int attackDamage = 1;
  public LayerMask attackLayer;

  public GameObject hitEffect;

  [Header("Audio Clips")]
  public AudioClip attackSlash;
  public AudioClip hitSound;
  public AudioSource audioSource2;
  public AudioClip takeDamageSound;
  public UnityEvent OnTakeDamage;

  bool attacking = false;
  bool readyToAttack = true;
  int attackCount;

  public void Attack(bool melee)
  {
    if (!readyToAttack || attacking) return;

    if (DialogSystemController.Instance.isDialogRunning)
    {
      return;
    }


    // if (GameManager.instance.stamina < attackStaminaCost)
    // {
    //   return;
    // }


    readyToAttack = false;
    attacking = true;

    Invoke(nameof(ResetAttack), attackSpeed);
    Invoke(nameof(AttackRaycast), attackDelay);
    if (melee)
    {
      audioSource.pitch = Random.Range(.9f, 1f);

      if (!weaponEquipped)
      {
        // GameManager.instance.SetStamina(-attackStaminaCost);

        audioSource.PlayOneShot(attackSlash);

        if (attackCount == 0)
        {
          animator.SetTrigger(PUSH_ENEMY_TRIGGER);
          attackCount++;
        }
        else
        {
          animator.SetTrigger(PUNCH_ENEMY_TRIGGER);
          attackCount = 0;
        }
      }
      else
      {
        GameManager.instance.SetStamina(-attackStaminaCostWeapon);

        audioSource.PlayOneShot(attackSlash);

        animator.SetTrigger(WEAPON_SLASH_TRIGGER);
        attackCount = 0;
      }
    }
    else
    {
      if (weaponEquipped)
      {
        animator.SetTrigger(THROW_TRIGGER);
        attackCount = 0;
        StartCoroutine("WaitToThrow");
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
    if (attackHitbox == null)
    {
      Debug.LogWarning("AttackHitbox nao configurada!");
      return;
    }
    var colliders = attackHitbox.GetObjectsInsideHitbox();

    foreach (var collider in colliders)
    {

      if (collider.transform.TryGetComponent(out Actor actor))
      {
        var dir = (collider.transform.position - MyCamera.transform.position).normalized;

        if (Physics.Raycast(MyCamera.transform.position, dir, out RaycastHit hit, 10, attackLayer))
        {
          HitTarget(hit.point);
        }

        actor.TakeDamage(attackDamage);

        if (!weaponEquipped)
        {
          actor.GetComponent<NavMeshAgent>().velocity = transform.forward * 7.5f;
        }
      }
    }
  }

  void HitTarget(Vector3 pos)
  {
    if (weaponEquipped)
    {
      audioSource.PlayOneShot(hitSound);
      //arrumar
      GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
      Destroy(GO, 20);
    }
    else
    {
      audioSource.PlayOneShot(hitSound);
      //arrumar
      GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
      Destroy(GO, 20);
    }
  }

  public void TakeDamage(int x)
  {
    if (GameManager.instance == null)
    {
      Debug.LogWarning("Game manager não encontrado pelo player ou nulo");
      return;
    }

    OnTakeDamage?.Invoke();

    GameManager.instance.SetHealth(x);
  }

  public void PlaySound()
  {
    audioSource.pitch = 1;
    audioSource.PlayOneShot(hitSound);
  }

  public void EquipWeapon(GameObject equippable)
  {
    if (_equipedWeapon == null)
    {
      attackDamage = equippable.GetComponent<EquipableObjects>().attackDamage;
      equippable.GetComponent<Rigidbody>().isKinematic = true;
      equippable.GetComponent<Rigidbody>().detectCollisions = false;
      equippable.transform.SetParent(_weaponSlot.transform, true);
      equippable.transform.localPosition = Vector3.zero;
      equippable.transform.rotation = _weaponSlot.rotation;
      _equipedWeapon = equippable;
      weaponEquipped = true;
    }
  }

  public void DropWeapon(bool _drop)
  {
    if (_drop)
    {
      if (_equipedWeapon != null)
      {
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
    if (_throw)
    {
      { Attack(false); }
    }
  }

  public void ThrowWeapon()
  {
    if (_equipedWeapon != null)
    {

      Rigidbody equippedRb = _equipedWeapon.GetComponent<Rigidbody>();

      _equipedWeapon.layer = 0;
      equippedRb.isKinematic = false;
      _equipedWeapon.transform.parent = null;

      if (equippedRb.TryGetComponent(out ThrowableObject throwableObject))
        equippedRb.AddForce(MyCamera.transform.forward * throwableObject.forceNeedToThrow);
      else
        equippedRb.AddForce(MyCamera.transform.forward * throwforce);

      _equipedWeapon.GetComponent<Rigidbody>().detectCollisions = true;

      if (_equipedWeapon.TryGetComponent(out DetectableSound detectableSound))
        detectableSound.SetCanMakeSound(true);

      _equipedWeapon = null;
      weaponEquipped = false;


    }
  }

  public void OpenMenu()
  {
    GameManager.instance.OpenCloseMenu(canvas);
  }

  IEnumerator WaitToThrow()
  {
    yield return new WaitForSeconds(.3f);
    ThrowWeapon();
  }

  public void DisableMovement(bool disable = true)
  {
    _disableMovement = disable;
    _rb.velocity = Vector3.zero;
    _rb.isKinematic = disable;
    _rb.useGravity = !disable;
  }

}
