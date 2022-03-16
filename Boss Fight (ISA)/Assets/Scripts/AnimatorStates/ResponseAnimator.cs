using UnityEngine;

public class ResponseAnimator : StateMachineBehaviour
{
    public void onPlayerAttackOver(bool isInteracting)
    {
        FindObjectOfType<CombatOneHanded>().onAttackOver(isInteracting);
    }

    public void onEnemyAttackOverMovement(int attackID)
    {
        FindObjectOfType<EnemyAIFSM>().AttackTarget(attackID, false);
    }

    public void onEnemyAttackOverInteracting(int attackID)
    {
        FindObjectOfType<EnemyAIFSM>().AttackTarget(attackID, true);
    }

    public void onEnemyAttackOver()
    {
        EnemyAIFSM enemy = FindObjectOfType<EnemyAIFSM>();

        if (enemy.targetInAttackRange())
        {
            int attackID = enemy.currentAttack == 2 ? 2 : enemy.currentAttack + 1;
            enemy.AttackTarget(attackID, true);
        }
        else
        {
            enemy.AttackTarget(0, false);
        }
    }

}
