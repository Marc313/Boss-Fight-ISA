using UnityEngine;
using UnityEngine.AI;

public class EnemyAIFSM : MonoBehaviour
{
    // --- Inspired by my Building Playful World Enemy script --- //

    [Header("Range")]
    public float sightRadius = 10f;       // The enemy will chase the target if it is in this radius
    public float losingRadius = 20f;      // The enemy will stop chasing the target if it is outside this radius

    [Header("Attack Stats")]
    public float attackDamage;

    public Transform target { get; private set; }
    public PlayerMovement player { get; private set; }

    public int currentAttack { get; private set; }
    public State state;
    private Animator anim;

    private FSM stateMachine;
    private NavMeshAgent agent;
    private Vector3 originalPosition;

    private void Awake()
    {
        stateMachine = GetComponent<FSM>();
        agent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<PlayerMovement>();
        target = player.transform;

        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        stateMachine?.onUpdate();
    }

    // Calculates the distance of this GameObject to its target
    float distanceToTarget()
    {
        return Vector3.Distance(target.position, transform.position);
    }

    public void chasePlayer()
    {
        agent.SetDestination(target.position);
    }

    public void returnToPosition()
    {
        agent.SetDestination(originalPosition);
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

    public void AttackTarget(int attackID)
    {
        Debug.Log($"Do attack {attackID} while current attack is {currentAttack}");
        currentAttack = attackID;
        anim.SetInteger("Attack", currentAttack);
    }
}
