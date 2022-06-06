using UnityEngine;

[CreateAssetMenu(menuName = "Magic/Magic Attack")]
public class MagicAttack : ScriptableObject
{
    public float range = 7f;
    public float moveSpeed = 5f;
    public float damage = 10f;
    public GameObject ProjectilePrefab;
    public GameObject ImpactParticles;

    private MagicProjectile projectile;

    public void ShootAttackTowardsPlayer(Vector3 senderPosition, Vector3 targetPosition)
    {
        Vector3 startPos = senderPosition;
        startPos.y = 0.01f;
        GameObject projectileObject = Instantiate(ProjectilePrefab, startPos, Quaternion.identity);
        projectile = projectileObject.GetComponent<MagicProjectile>();

        if(projectile == null) return;

        projectile.moveSpeed = moveSpeed;
        projectile.damage = damage;
        projectile.lifeTime = range;
        projectile.ImpactParticles = ImpactParticles;
        SetProjectileTarget(targetPosition);
    }

    public void SetProjectileTarget(Vector3 targetPosition)
    {
        projectile.SetTarget(targetPosition);
    }
}