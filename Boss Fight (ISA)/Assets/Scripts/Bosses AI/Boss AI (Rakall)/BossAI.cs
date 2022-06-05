using UnityEngine;
using UnityEngine.AI;

public class BossAI : Movement
{
    // --- Inspired by my Building Playful World Enemy script --- //

    [Header("Range")]
    public float sightRadius = 10f;       // The enemy will chase the target if it is in this radius
    public float losingRadius = 20f;      // The enemy will stop chasing the target if it is outside this radius

    public Transform target { get; private set; }
    public PlayerMovement player { get; private set; }

    [HideInInspector] public State state;

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

    protected override void Update()
    {
        if(GameManager.state == GameManager.GameState.FIGHT)
        {
            stateMachine?.onUpdate();
        }
    }

    // Movement
    public void UpdateRunningValue()
    {
        anim.SetFloat("Vertical", agent.velocity.x);
        anim.SetFloat("Horizontal", agent.velocity.z);
    }

    public void EnterRestingState(float attackCooldown)
    {
        stateMachine.SwitchState(typeof(RestingStateBoss));
    }

    public void MoveAwayFromPlayer()
    {
        Vector3 moveDirection = (transform.position - target.position).normalized;
        moveDirection.y = 0;
        Vector3 movement = moveDirection * currentMoveSpeed * Time.deltaTime;

        //rb.position += movement;

        agent.enabled = true;
        agent.stoppingDistance = 0;
        agent.SetDestination(transform.position + moveDirection);
        //agent.Move(movement);
    }

    #region FSM
    // Calculates the distance of this GameObject to its target
    float distanceToTarget()
    {
        return Vector3.Distance(target.position, transform.position);
    }

    public void chasePlayer()
    {
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
        return distance <= agent.stoppingDistance;
    }
    #endregion
}
