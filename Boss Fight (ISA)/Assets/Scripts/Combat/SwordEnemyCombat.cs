using UnityEngine;

public class SwordEnemyCombat : Combat
{
    private SwordEnemyAI enemyAI;

    protected override void Awake()
    {
        base.Awake();
        enemyAI = GetComponent<SwordEnemyAI>();
    }

    public override void OverlapSphere(Attack attack, Transform hitbox)
    {
        Collider[] colliders = Physics.OverlapSphere(hitbox.position, 1f);

        foreach (Collider collider in colliders)
        {
            // Check for each collider if the collider is a player
            PlayerStats playerStats = collider.GetComponent<PlayerStats>();
            PlayerCombat playerCombat = collider.GetComponent<PlayerCombat>();

            if (playerCombat == null && playerStats == null) continue;

            if (playerCombat == null)
            {
                // No PlayerCombat? Take full damage.
                playerStats.takeDamage(attack.damage);
                break;
            } 
            else if (playerStats == null)
            {
                // No PlayerStats? Still Stagger
                if (playerCombat.IsShieldBashing)
                {
                    Stagger();
                    Debug.Log("Shield Bash!!");
                    break;
                }
                continue;           // Continue to next target if the target was not shieldbashing
            }

            // If both are not null, check all options.
            if (!playerCombat.IsBlocking)
            {
                playerStats.takeDamage(attack.damage);
            }
            else if (playerCombat.IsBlocking && !playerCombat.IsShieldBashing)
            {
                playerStats.takeDamage(attack.damage / 5);
                Debug.Log("Block!");
            }
            else if (playerCombat.IsShieldBashing)
            {
                Stagger();
                Debug.Log("Shield Bash!!");
            }

            break;
        }
    }

    public void PerformNextAttack()
    {
        if (enemyAI.targetInAttackRange())
        {
            int attackID = currentAttack + 1;
            PerformAttack(attackID, true);
        }
        else
        {
            OnAttackOver(false);
        }
    }
}