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
        enemyAI.chasePlayer();

        if(enemyAI.targetOutOfSight())
        {
            fsm.SwitchState(typeof(IdleState));
        }

        if(enemyAI.targetInAttackRange())
        {
            fsm.SwitchState(typeof(AttackState));
        }
    }

    public override int getNumber() { return 1; }
}
