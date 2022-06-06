using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float damage;
    [HideInInspector] public float lifeTime;
    [HideInInspector] public GameObject ImpactParticles;

    private ParticleSystem trailParticles;
    private Vector3 targetDirection;
    private bool isExploded;

    private void Awake()
    {
        trailParticles = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        trailParticles.Play();
    }

    private void Update()
    {
        if (isExploded) return;

        if (targetDirection != null)
        {
            ShootTowards(targetDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isExploded) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if(stats != null)
        {
            Debug.Log("COLLISION");
            TriggerExplosion();
            stats.takeDamage(damage);
        }
    }

    private void ShootTowards(Vector3 direction)
    {
        Vector3 movement = direction * Time.deltaTime * moveSpeed;
        transform.position += movement;

        lifeTime -= movement.magnitude;
        if (lifeTime <= 0) TriggerExplosion();
    }

    public void SetTarget(Vector3 targetPosition)
    {
        targetDirection = (targetPosition - transform.position).normalized;
    }

    private void TriggerExplosion()
    {
        isExploded = true;
        //trailParticles.Stop();

        Instantiate(ImpactParticles, transform.position, ImpactParticles.transform.rotation, transform);

        Destroy(this.gameObject, 1.6f);
    }
}