using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Transform hitbox;
    private CombatOneHanded combat;

    private void Awake()
    {
        combat = GetComponentInParent<CombatOneHanded>();
    }

    public void EnableSwordCollider()
    {
        if (GetComponentInChildren<DamageCollider>() == null) Debug.Log("'_'");
        GetComponentInChildren<DamageCollider>()?.EnableCollider();
    }

    public void DisableSwordCollider()
    {
        GetComponentInChildren<DamageCollider>()?.DisableCollider();
    }

    public void OverlapSphere()
    {
        Collider[] colliders = Physics.OverlapSphere(hitbox.position, 1f);

        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider.gameObject.name);
            EnemyStats enemyStats = collider.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.takeDamage(20f);
                break;
            }
        }
    }

    public void ShieldBashStart()
    {
        combat.IsShieldBashing = true;
    }

    public void ShieldBashEnd()
    {
        combat.IsShieldBashing = false;
    }
}
