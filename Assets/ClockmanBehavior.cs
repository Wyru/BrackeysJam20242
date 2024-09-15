using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ClockmanBehavior : MonoBehaviour
{

  [Header("Moviment")]
  public float maxChaseDistance = 30f;
  public float normalWakSpeed = 1.5f;
  public float chasingWalkSpeed = 1.5f;
  public float maxMovementDistance = 10;
  public float movementDecreaseRate = .1f;
  public float chasingPlayerSpeedModifier = 3;
  public float normalPlayerSpeedModifier = 3;

  [Header("Debug")]
  [SerializeField]
  float currentMaxMovement;

  [SerializeField]
  bool canAttack;

  bool spottedPlayer = false;

  [Header("Detection")]
  public float farMaxDetectionDistance = 10f;
  public float farDetectionViewAngle = 45f;
  public float nearMaxDetectionDistance = 10f;
  public float nearDetectionViewAngle = 45f;

  [Range(0, 1)]
  public float spontaneousChaseStateProbability = .1f;


  [Range(0, 1)]
  public float searchPlayerAnimeProbability = .4f;


  [Header("Attack")]

  public int attackPower = 20;
  public float minAttackDistance = 2f;
  public float minAttackAngle = 15f;
  public float playerPushForce = 100f;

  [Range(0, 1)]
  public float strongAttackProbability;

  [Header("Damage")]
  public int numberOfHitsToDamageAni = 3;
  int hitCount = 0;

  [Header("References")]
  public Animator animator;
  public BoxCollider hitCollider;
  public NavMeshAgent agent;
  public Transform eyesAnchor;

  public AudioSource clock01;
  public AudioSource clock02;

  public FootstepController footstepController;

  [Header("References Timers")]
  public Timer detectPlayerTimer;
  public Timer movementTimer;
  public Timer deadTimer;

  [Header("Events")]
  public UnityEvent OnDetectPlayerEvent;
  public UnityEvent OnTakeDamageEvent;
  public UnityEvent OnAttackEvent;
  public UnityEvent OnDeathEvent;
  public UnityEvent OnLostSightOfPlayer;

  public UnityEvent OnRevive;

  public Transform player;

  Vector3 startOfMoviment;

  public LayerMask playerLayerMask;



  public enum State
  {
    Idle,
    Walking,
    Chasing,
    ChasingSound,
    Attacking,
    Damage,
    Dead,
  }


  public State state = State.Idle;

  bool firstClock = false;

  private void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    movementTimer.OnTimerEnd += OnMovementTimerEnd;
    movementTimer.StartTimer(true);
  }

  void Start()
  {
    player = FindAnyObjectByType<PlayerController>().transform;

    state = State.Walking;
  }

  public float distanceToPlayer = 0;

  public Vector3 soundPosition;


  void Update()
  {

    distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

    switch (state)
    {
      case State.Idle:
        animator.SetBool("Walking", false);
        agent.isStopped = true;
        UpdateVision();
        break;




      case State.Walking:
        footstepController.isRunning = false;
        CheckDistanceTraveled();
        UpdateVision();
        break;



      case State.Attacking:
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        if (isAniEnd("WeakAttack") &&
            isAniEnd("StrongAttack"))
        {
          ChangeState(State.Chasing);
        }

        break;



      case State.Chasing:
        footstepController.isRunning = true;
        agent.destination = player.position;

        CheckDistanceTraveled();

        if (distanceToPlayer > maxChaseDistance)
        {
          ChangeState(State.Walking);
          LostSightOfPlayer();
        }

        if (distanceToPlayer <= minAttackDistance)
        {
          if (canAttack)
            Attack();
        }

        break;

      case State.Damage:

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
          ChangeState(State.Chasing);
        }
        break;

      case State.Dead:
        if (deadTimer.Timeout)
        {
          Revive();
        }
        break;

      default:
        return;
    }
  }

  public void OnDeath()
  {
    if (state == State.Dead)
      return;
    deadTimer.StartTimer();
    agent.isStopped = true;
    hitCollider.enabled = false;
    animator.SetTrigger("Death");
    ChangeState(State.Dead);
    OnDeathEvent?.Invoke();
  }

  void Revive()
  {
    hitCollider.enabled = true;
    ChangeState(State.Walking);
    OnRevive?.Invoke();
    animator.SetTrigger("Revive");
  }

  public void OnTakeDamage()
  {
    if (state == State.Dead)
      return;


    hitCount++;
    if (hitCount >= numberOfHitsToDamageAni)
    {
      OnTakeDamageEvent?.Invoke();
      ChangeState(State.Damage);
      hitCount = 0;
      animator.SetTrigger("Damage");
      firstClock = true;
    }

  }

  public AttackCollider hitbox;

  void OnDetectPlayer()
  {
    OnDetectPlayerEvent?.Invoke();
    ChangeState(State.Chasing);
    animator.SetTrigger("FoundPlayer");
    currentMaxMovement = maxMovementDistance;
  }

  public void UpdateVision()
  {
    if (IsPlayerInsideActionRange(farMaxDetectionDistance, farDetectionViewAngle) ||
        IsPlayerInsideActionRange(nearMaxDetectionDistance, nearDetectionViewAngle))
    {
      if (spottedPlayer == false)
      {
        spottedPlayer = true;
        detectPlayerTimer.StartTimer();
      }
      else
      {
        if (detectPlayerTimer.Timeout)
        {
          OnDetectPlayer();
        }
      }

      return;
    }

    spottedPlayer = false;
  }

  bool IsPlayerInsideActionRange(float maxDistance, float angle)
  {
    if (player == null)
      Debug.LogWarning("Player component not found!");

    Vector3 playerDirection = (player.transform.position - eyesAnchor.position).normalized;

    // distancia mínima
    if (distanceToPlayer > maxDistance)
      return false;

    // angulo de visao
    float angleBetween = Vector3.Angle(transform.forward, playerDirection);

    if (angleBetween > angle / 2)
      return false;

    // se consegue ver
    return Physics.Raycast(eyesAnchor.position, playerDirection, maxDistance, playerLayerMask);

  }

  void ChangeState(State newState)
  {
    Debug.Log($"{name} state changed from {state} to {newState}");
    animator.SetBool("Walking", false);
    SetSpeed();
    state = newState;
  }

  void SetSpeed(bool fast = false)
  {
    if (fast)
    {
      animator.speed = chasingPlayerSpeedModifier;
      agent.speed = normalWakSpeed * chasingPlayerSpeedModifier;
    }
    else
    {
      animator.speed = 1;
      agent.speed = normalWakSpeed;
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    // Generate the cone mesh and draw it
    Mesh coneMesh = MeshFactory.GenerateVisionConeMesh(farDetectionViewAngle, farMaxDetectionDistance, 20);
    Gizmos.DrawMesh(coneMesh, transform.position + Vector3.up, transform.rotation);

    Gizmos.color = Color.cyan;
    // Generate the cone mesh and draw it
    coneMesh = MeshFactory.GenerateVisionConeMesh(nearDetectionViewAngle, nearMaxDetectionDistance, 20);
    Gizmos.DrawMesh(coneMesh, transform.position + (Vector3.up * 1.1f), transform.rotation);

    Gizmos.color = Color.red;
    // Generate the cone mesh and draw it
    coneMesh = MeshFactory.GenerateVisionConeMesh(minAttackAngle, minAttackDistance, 10);
    Gizmos.DrawMesh(coneMesh, transform.position + (Vector3.up * 1.15f), transform.rotation);

    if (player)
      Gizmos.DrawLine(eyesAnchor.position, player.position);
  }

  void MoveTowardsPlayer()
  {

    SetSpeed(state == State.Chasing);
    animator.SetBool("Walking", true);
    agent.isStopped = false;
    startOfMoviment = transform.position;
    agent.destination = player.position;
    footstepController.isWalking = true;
  }

  void StopMovement()
  {
    animator.SetBool("Walking", false);
    agent.isStopped = true;
    SetSpeed(false);
    footstepController.isWalking = false;
  }

  void Attack()
  {
    canAttack = false;
    OnAttackEvent?.Invoke();
    StopMovement();
    ChangeState(State.Attacking);
    if (ShouldPerformAction(strongAttackProbability))
    {
      animator.SetTrigger("AttackStrong");
      return;
    }
    animator.SetTrigger("AttackWeak");

    Invoke("RunHitbox", .3f);
  }

  void RunHitbox()
  {
    var colliders = hitbox.GetObjectsInsideHitbox();
    foreach (var collider in colliders)
    {
      Debug.Log(collider.name);

      if (collider.gameObject.CompareTag("Player"))
      {
        if (collider.gameObject.TryGetComponent(out PlayerController player))
        {
          DealDamageToPlayer();
        }
      }
    }
  }

  void DealDamageToPlayer()
  {
    PlayerController.instance.TakeDamage(-attackPower);
    PlayerController.instance.GetComponent<Rigidbody>().AddForce(transform.forward * playerPushForce, ForceMode.Impulse);
  }

  public void OnMovementTimerEnd()
  {
    firstClock = !firstClock;

    if (!firstClock)
    {
      //movement clock action
      if (state != State.Dead)
        clock01.PlayOneShot(clock01.clip);

      if (state == State.Chasing)
      {
        currentMaxMovement *= 1 - movementDecreaseRate;

        if (currentMaxMovement < 1)
        {
          StopChasePlayer();
          return;
        }

        MoveTowardsPlayer();
        return;
      }

      if (state == State.Walking)
      {
        if (ShouldPerformAction(searchPlayerAnimeProbability))
        {
          animator.SetTrigger("Alert");
          return;
        }

        MoveTowardsPlayer();
      }

      return;
    }

    // fixed clock action
    if (state != State.Dead)
      clock02.PlayOneShot(clock02.clip);

    StopMovement();

    canAttack = true;

    if (IsPlayerInsideActionRange(minAttackDistance, minAttackAngle))
    {
      Attack();
      return;
    }
  }

  void StopChasePlayer()
  {
    Debug.Log("StopChasePlayer");
    // teletransportar?
    LostSightOfPlayer();
    ChangeState(State.Walking);
  }

  void CheckDistanceTraveled()
  {
    float distanceTraveled = Vector3.Distance(startOfMoviment, transform.position);

    var x = state == State.Chasing ? currentMaxMovement : maxMovementDistance;

    if (distanceTraveled > x)
    {
      StopMovement();
    }
  }


  void LostSightOfPlayer()
  {
    OnLostSightOfPlayer?.Invoke();
  }

  bool ShouldPerformAction(float chance)
  {
    float randomValue = Random.Range(0f, 1f);
    return randomValue < chance;
  }

  bool isAniEnd(string _name)
  {
    return !animator.GetCurrentAnimatorStateInfo(0).IsName(_name);
  }
}
