using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float damage;
    [HideInInspector] public float lifeTime;
    [HideInInspector] public float UpdateTargetRate;
    [HideInInspector] public GameObject ImpactParticles;

    private ParticleSystem trailParticles;
    private Transform target;
    private Vector3 targetDirection;
    private bool isExploded;

    private void Awake()
    {
        trailParticles = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        trailParticles.Play();
        InvokeRepeating(nameof(SetNewTargetDirection), 0, UpdateTargetRate);
    }

    private void FixedUpdate()
    {
        if (isExploded) return;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) TriggerExplosion();

        if (targetDirection != null)
        {
            ShootTowards(targetDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isExploded) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();

        if (stats != null)
        {
            TriggerExplosion();
            stats.takeDamage(damage);
        }
    }

    private void ShootTowards(Vector3 direction)
    {
        Vector3 movement = direction * Time.deltaTime * moveSpeed;
        transform.position += movement;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void SetNewTargetDirection()
    {
        if (target == null) return;
        targetDirection = (target.transform.position - transform.position).normalized;
        targetDirection.y = 0; // Stay on the ground
    }

    private void TriggerExplosion()
    {
        CancelInvoke(nameof(SetNewTargetDirection));
        isExploded = true;
        Instantiate(ImpactParticles, transform.position, ImpactParticles.transform.rotation, transform);

        Destroy(this.gameObject, 1.6f);
    }
}