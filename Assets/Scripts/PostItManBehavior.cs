using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PostItManBehavior : MonoBehaviour
{

  [Header("Moviment")]
  public float maxChaseDistance = 30f;
  public float normalWakSpeed = 1.5f;
  public float chasingPlayerSpeedModifier = 3;
  public Transform[] waypoints;

  [Header("Detection")]

  public float farMaxDetectionDistance = 10f;
  public float farDetectionViewAngle = 45f;

  public float nearMaxDetectionDistance = 10f;
  public float nearDetectionViewAngle = 45f;

  public LayerMask playerLayer;


  [Header("SoundDetection")]
  public int attentionThreshold = 10;

  [Header("Attack")]
  public float minAttackDistance = 2f;
  public float minAttackAngle = 15f;



  [Header("References")]
  public Animator animator;
  public BoxCollider hitCollider;
  public NavMeshAgent agent;
  public Transform eyesAnchor;
  public SoundDetectionBehavior soundDetectionBehavior;
  public FootstepController footstepController;
  public Timer idleTimer;
  public Timer attackCooldownTimer;
  public Timer deadTimer;
  public Timer detectPlayerTimer;



  [Header("Events")]
  public UnityEvent OnDetectPlayerEvent;
  public UnityEvent OnTakeDamageEvent;
  public UnityEvent OnAttackEvent;
  public UnityEvent OnDeathEvent;
  public UnityEvent OnHearEvent;



  public Transform player;


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


  private void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    soundDetectionBehavior.OnHearObject += OnHearObject;
  }

  public int currentWaypoint = 0;
  void Start()
  {
    player = FindAnyObjectByType<PlayerController>().transform;
  }

  public float timeCouter = 0f;

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
        if (idleTimer.Timeout)
        {
          ChangeState(State.Walking);
          return;
        }

        break;

      case State.Walking:
        if (waypoints != null && waypoints.Length > 0)
        {
          animator.SetBool("Walking", true);
          agent.isStopped = false;
          agent.destination = waypoints[currentWaypoint].position;
          footstepController.isWalking = true;


          if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= agent.stoppingDistance + 0.1f)
          {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            ChangeState(State.Idle);
            idleTimer.StartTimer();
          }
        }
        UpdateVision();
        // validar se está vendo o player
        break;
      case State.Attacking:
        // ataque
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && attackCooldownTimer.Timeout)
        {
          ChangeState(State.Chasing);
        }

        break;

      case State.Chasing:
        animator.SetBool("Walking", true);
        SetSpeed(true);
        agent.isStopped = false;
        agent.destination = player.transform.position;
        footstepController.isWalking = true;
        footstepController.isRunning = true;

        if (IsPlayerInsideActionRange(minAttackDistance, minAttackAngle) && attackCooldownTimer.Timeout)
        {
          animator.SetTrigger("Attack");
          attackCooldownTimer.StartTimer();
          ChangeState(State.Attacking);
          OnAttackEvent?.Invoke();
          return;
        }

        if (distanceToPlayer > maxChaseDistance)
          ChangeState(State.Walking);
        break;

      case State.ChasingSound:
        agent.isStopped = false;
        UpdateVision();
        SetSpeed(true);
        animator.SetBool("Walking", true);
        agent.destination = soundPosition;
        footstepController.isRunning = true;
        footstepController.isWalking = true;


        if (Vector3.Distance(transform.position, soundPosition) < 1)
        {
          ChangeState(State.Idle);
          idleTimer.StartTimer();
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

  public void Revive()
  {
    hitCollider.enabled = true;
    ChangeState(State.Chasing);
    animator.SetTrigger("Revive");

  }

  public void OnTakeDamage()
  {
    if (state == State.Dead)
      return;

    OnTakeDamageEvent?.Invoke();
    animator.SetTrigger("Damage");
    ChangeState(State.Damage);
  }

  bool spottedPlayer = false;
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
          Debug.Log("Has spot player!");
          OnDetectPlayerEvent?.Invoke();
          ChangeState(State.Chasing);
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
    if (Physics.Raycast(eyesAnchor.position, playerDirection, out RaycastHit hit, maxDistance, playerLayer))
    {
      return true;
    }
    // raycast da visão ta bugado
    return false;
  }

  void ChangeState(State newState)
  {
    // Debug.Log($"{name} state changed from {state} to {newState}");
    animator.SetBool("Walking", false);
    footstepController.isWalking = false;
    footstepController.isRunning = false;
    SetSpeed();
    state = newState;
  }


  public void OnHearObject(Vector3 position, int volume)
  {
    if (state == State.Idle || state == State.Walking)
    {
      if (attentionThreshold < volume)
      {
        ChangeState(State.ChasingSound);
        soundPosition = position;
        OnHearEvent?.Invoke();
      }
    }
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
}
