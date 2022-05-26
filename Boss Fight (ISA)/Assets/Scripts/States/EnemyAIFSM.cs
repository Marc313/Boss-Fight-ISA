using UnityEngine;
using UnityEngine.AI;

public class EnemyAIFSM : Movement
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

    private EnemyCombat combat;

    private void Awake()
    {
        stateMachine = GetComponent<FSM>();
        agent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<PlayerMovement>();
        target = player.transform;

        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(GameManager.state != GameManager.GameState.FIGHT)
        {
            isInteracting = true;
        }

        if(!isInteracting)
        {
            stateMachine?.onUpdate();
        }
    }

    /*public void AttackTarget(int attackID, bool isInteracting)
    {
        //this.isInteracting = isInteracting;
        currentAttack = attackID;
        anim.SetInteger("Attack", currentAttack);
    }*/

    /*public void Stagger()
    {
        this.isInteracting = true;
        currentAttack = 0;
        anim.SetBool("IsStaggering", true);
    }

    public void OnStaggerOver()
    {
        this.isInteracting = false;
        anim.SetBool("IsStaggering", false);
    }*/

    /*public void StartRunningAnimation()
    {
        anim.SetFloat("Vertical", 1);
    }

    public void EndRunningAnimation()
    {
        anim.SetFloat("Vertical", 0);
    }*/

    // Movement
    public void UpdateRunningValue()
    {
        anim.SetFloat("Vertical", agent.velocity.x);
        anim.SetFloat("Horizontal", agent.velocity.z);
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
