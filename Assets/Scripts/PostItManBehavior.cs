using UnityEngine;
using UnityEngine.AI;

public class PostItManBehavior : MonoBehaviour
{

  [Header("Moviment")]
  public float waitTime = 5f;
  public Transform[] waypoints;

  [Header("Attack")]

  public float minAttackDistance = 2f;
  public float maxChaseDistance = 2f;


  [Header("References")]
  public Animator animator;
  public BoxCollider hitCollider;
  public NavMeshAgent agent;


  public Transform player;


  public enum State
  {
    Idle,
    Walking,
    Chasing,
    Attacking,
    Damage,
    Dead
  }

  [Header("Status")]

  public State state = State.Idle;

  public bool isChasingPlayer = false;


  private void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
  }

  public int currentWaypoint = 0;
  void Start()
  {
    player = FindAnyObjectByType<PlayerController>().transform;
  }

  public float timeCouter = 0f;

  void Update()
  {
    switch (state)
    {
      case State.Idle:
        if (timeCouter < 0)
        {
          state = State.Walking;
          return;
        }
        agent.isStopped = false;
        animator.SetBool("Walking", false);
        timeCouter -= Time.deltaTime;


        // esperar um tempo
        // validar se está vendo o player
        break;

      case State.Walking:
        if (waypoints != null && waypoints.Length > 0)
        {
          animator.SetBool("Walking", true);
          agent.isStopped = false;
          agent.destination = waypoints[currentWaypoint].position;

          if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1)
          {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            state = State.Idle;
            timeCouter = waitTime;
          }
        }
        // validar se está vendo o player
        break;
      case State.Attacking:
        // ataque
        agent.isStopped = true;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
          state = State.Chasing;
        }

        break;

      case State.Chasing:
        // ataque
        // validação de danodo
        animator.SetBool("Walking", true);
        agent.isStopped = false;
        agent.destination = player.transform.position;
        var distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < minAttackDistance)
        {
          animator.SetTrigger("Attack");
          state = State.Attacking;
        }

        if (distance > maxChaseDistance)
        {
          state = State.Walking;
        }


        break;

      case State.Damage:

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
          state = State.Chasing;
        }

        break;

      case State.Dead:

        break;

      default:
        return;
    }

  }


  public void OnDeath()
  {
    if (state == State.Dead)
      return;

    agent.isStopped = true;
    hitCollider.enabled = false;
    animator.SetTrigger("Death");
    state = State.Dead;
  }

  public void OnTakeDamage()
  {
    if (state == State.Dead)
      return;

    animator.SetTrigger("Damage");
    state = State.Damage;
  }

}
