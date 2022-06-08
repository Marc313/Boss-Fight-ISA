using UnityEngine;

[CreateAssetMenu(menuName = "Magic/Magic Attack")]
public class MagicAttack : ScriptableObject
{
    public float range = 7f;
    public float moveSpeed = 5f;
    public float damage = 10f;
    public float UpdateTargetRate = .5f;
    public GameObject ProjectilePrefab;
    public GameObject ImpactParticles;

    private MagicProjectile projectile;

    public void ShootAttackTowardsPlayer(Vector3 senderPosition, Transform target)
    {
        Vector3 startPos = senderPosition;
        startPos.y = 0.001f;
        GameObject projectileObject = Instantiate(ProjectilePrefab, startPos, Quaternion.identity);
        projectile = projectileObject.GetComponent<MagicProjectile>();

        if (projectile == null) return;

        AssignProjectileValues();
        SetProjectileTarget(target);
    }

    private void SetProjectileTarget(Transform target)
    {
        projectile.SetTarget(target);
    }

    private void AssignProjectileValues()
    {
        projectile.moveSpeed = moveSpeed;
        projectile.damage = damage;
        projectile.lifeTime = range;
        projectile.ImpactParticles = ImpactParticles;
        projectile.UpdateTargetRate = UpdateTargetRate;
    }
}