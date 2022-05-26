using UnityEngine;

public class ChaseState : State
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
            fsm.SwitchState(typeof(IdleState));
            return;
        }

        if(enemyAI.targetInAttackRange())
        {
            fsm.SwitchState(typeof(AttackState));
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
