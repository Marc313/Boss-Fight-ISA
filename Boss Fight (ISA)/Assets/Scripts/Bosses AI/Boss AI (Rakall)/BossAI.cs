using UnityEngine;
using UnityEngine.AI;

public class BossAI : Movement
{
    // --- Inspired by my Building Playful World Enemy script --- //

    [Header("Range")]
    public float attackRange = 3f;
    public float restingRange = 6f;
    public float sightRadius = 10f;       // The enemy will chase the target if it is in this radius
    public float losingRadius = 20f;      // The enemy will stop chasing the target if it is outside this radius

    public Transform target { get; private set; }
    public PlayerMovement player { get; private set; }

    [HideInInspector] public State state;

    [HideInInspector] public bool isInCombo;

    private FSM stateMachine;
    private NavMeshAgent agent;
    private Rigidbody rb;

    private SwordEnemyCombat combat;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = GetComponent<FSM>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        player = FindObjectOfType<PlayerMovement>();
        target = player.transform;
    }

    private void OnEnable()
    {
        GameManager.OnStateChange += OnGameStateChange;
    }
    private void OnDisable()
    {
        GameManager.OnStateChange -= OnGameStateChange;
    }

    protected override void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.FIGHT)
        {
            stateMachine?.OnUpdate();
        }
    }

    public void UpdateRunningValue()
    {
        float vert = 0; float hor = 0;

        Vector3 moveDirection = agent.destination - transform.position;
        float destinationAngle = Vector3.Angle(moveDirection, transform.forward);

        if(destinationAngle < 45)
        {
            vert = 1;
            hor = 0;
        } else if(destinationAngle < 90)
        {
            vert = 1;
            hor = 1;
        } else if (destinationAngle < 135)
        {
            vert = -1;
            hor = 1;
        }
        else
        {
            vert = -1;
            hor = 0;
        }

        anim.SetFloat("Vertical", vert);
        anim.SetFloat("Horizontal", hor);
    }

    public void EnterRestingState(float attackCooldown)
    {
        stateMachine.SwitchState(typeof(RestingStateBoss));
    }

    public void MoveAwayFromPlayer()
    {
        agent.enabled = true;
        agent.stoppingDistance = 0;

        Vector3 playerDirection = (target.position - transform.position).normalized;
        Vector3 moveDirection;

        if (targetInRestingRange())
        {
            moveDirection = -playerDirection;
        }
        else
        {
            moveDirection = playerDirection;
        }

        moveDirection.y = 0;

        agent.SetDestination(transform.position + moveDirection);
        transform.rotation = Quaternion.LookRotation(playerDirection);
    }

    private void OnGameStateChange(GameManager.GameState newState)
    {
        if (newState != GameManager.GameState.FIGHT)
        {
            agent.enabled = false;
            stateMachine?.ExitCurrentState();       // Only use AI states during the Fight.
        }

        if (newState == GameManager.GameState.LOST)
        {
            StopAllCoroutines();
            anim.SetTrigger("Dance");
        }
    }

    #region FSM
    // Calculates the distance of this GameObject to its target
    float distanceToTarget()
    {
        return Vector3.Distance(target.position, transform.position);
    }

    public void chasePlayer()
    {
        agent.stoppingDistance = attackRange;
        agent.SetDestination(target.position);
    }

    public void stopChase()
    {
        agent.enabled = false;
    }

    public void continueChase()
    {
        agent.enabled = true;
    }

    public void EnableAgentRotation(bool boolean)
    {
        agent.updateRotation = boolean;
    }

    public bool targetInSight()
    {
        float distance = distanceToTarget();
        return distance <= sightRadius;
    }

    public bool targetOutOfSight()
    {
        float distance = distanceToTarget();
        return distance > losingRadius;
    }

    public bool targetInAttackRange()
    {
        float distance = distanceToTarget();
        return distance <= attackRange;
    }

    public bool targetInRestingRange()
    {
        float distance = distanceToTarget();
        return distance <= restingRange;
    }
    #endregion
}
