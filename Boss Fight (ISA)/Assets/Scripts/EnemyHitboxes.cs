using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxes : MonoBehaviour
{
    public Transform hitbox;
    public EnemyStats stats;
    public EnemyAIFSM EnemyAi;

    private void Awake()
    {
        stats = GetComponentInParent<EnemyStats>();
        EnemyAi = GetComponentInParent<EnemyAIFSM>();
    }

    public void OverlapSphere()
    {
        Collider[] colliders = Physics.OverlapSphere(hitbox.position, 1f);

        foreach (Collider collider in colliders)
        {
            // Check for each collider if the collider is a player
            PlayerStats playerStats = collider.GetComponent<PlayerStats>();

            if(playerStats != null)
            {
                // TODO: Refactor
                CombatOneHanded playerCombat = collider.GetComponent<CombatOneHanded>();
                if(playerCombat != null)
                {
                    if (!playerCombat.IsBlocking)
                    {
                        playerStats.takeDamage(10f); // Damage the player
                    }
                    else if (playerCombat.IsBlocking && !playerCombat.IsShieldBashing) {
                        playerStats.takeDamage(5f);
                        Debug.Log("Block!");
                    }
                    else if (playerCombat.IsShieldBashing)
                    {
                        EnemyAi.Stagger();
                        Debug.Log("Shield Bash!!");
                    }
                }
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitbox.position, 1f);
    }
}
