using UnityEngine;

public class ChaseSwordEnemy : State
{
   public override void onEnter()
    {
    }

    public override void onExit()
    {
    }

    public override void onUpdate()
    {
        if (enemyAI.targetOutOfSight())
        {
            fsm.SwitchState(typeof(IdleSwordEnemy));
            return;
        }

        if(enemyAI.targetInAttackRange())
        {
            fsm.SwitchState(typeof(SwordEnemyAttackState));
            return;
        }

        if (enemyAI.isInteracting)
        {
            enemyAI.stopChase();
        }
        else
        {
            enemyAI.continueChase();
            enemyAI.chasePlayer();
        }

        enemyAI.UpdateRunningValue();
    }
}
