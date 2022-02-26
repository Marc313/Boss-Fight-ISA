using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();

        collider.isTrigger = true;
        collider.enabled = false;
    }

    public void EnableCollider()
    {
        collider.enabled = true;
    }

    public void DisableCollider()
    {
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyStats enemy = other.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            enemy.takeDamage(20);
        }
    }
}
