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
  public float minAttackDistance = 2f;
  public float minAttackAngle = 15f;

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
  public Timer idleTimer;
  public Timer movementTimer;
  public Timer deadTimer;

  [Header("Events")]
  public UnityEvent OnDetectPlayerEvent;
  public UnityEvent OnTakeDamageEvent;
  public UnityEvent OnAttackEvent;
  public UnityEvent OnDeathEvent;
  public UnityEvent OnHearEvent;

  public Transform player;

  Vector3 startOfMoviment;


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

  [Header("Status")]

  public State state = State.Idle;

  public bool isChasingPlayer = false;


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

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AttackWeak") && !animator.GetCurrentAnimatorStateInfo(0).IsName("AttackStrong"))
        {
          ChangeState(State.Chasing);
        }

        break;

      case State.Chasing:
        footstepController.isRunning = true;
        agent.destination = player.position;

        CheckDistanceTraveled();

        if (distanceToPlayer > maxChaseDistance)
          ChangeState(State.Walking);
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
    ChangeState(State.Chasing);
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

  void OnDetectPlayer()
  {
    OnDetectPlayerEvent?.Invoke();
    ChangeState(State.Chasing);
    animator.SetTrigger("FoundPlayer");
  }

  public void UpdateVision()
  {
    if (IsPlayerInsideActionRange(farMaxDetectionDistance, farDetectionViewAngle) ||
        IsPlayerInsideActionRange(nearMaxDetectionDistance, nearDetectionViewAngle))
    {
      Debug.Log("Has spot player!");
      OnDetectPlayer();
    }
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

    Debug.Log("raycast");


    // se consegue ver
    if (Physics.Raycast(eyesAnchor.position, playerDirection, out RaycastHit hit, maxDistance))
    {
      Debug.Log(hit.collider.gameObject.name);
      if (hit.collider.transform == player)
      {
        return true;
      }
    }
    // raycast da visão ta bugado
    return false;
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

    if (ShouldPerformAction(strongAttackProbability))
    {
      animator.SetTrigger("AttackStrong");
      return;
    }
    animator.SetTrigger("AttackWeak");
    ChangeState(State.Attacking);
  }

  public void OnMovementTimerEnd()
  {
    firstClock = !firstClock;


    if (!firstClock)
    {
      //movement clock action
      clock01.PlayOneShot(clock01.clip);

      if (state == State.Chasing)
      {
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
    clock02.PlayOneShot(clock02.clip);
    StopMovement();

    if (IsPlayerInsideActionRange(minAttackDistance, minAttackAngle))
    {
      Attack();
      return;
    }
  }


  void CheckDistanceTraveled()
  {
    float distanceTraveled = Vector3.Distance(startOfMoviment, transform.position);

    if (distanceTraveled > maxMovementDistance)
    {
      StopMovement();
    }
  }

  bool ShouldPerformAction(float chance)
  {
    float randomValue = Random.Range(0f, 1f);
    return randomValue < chance;
  }
}
