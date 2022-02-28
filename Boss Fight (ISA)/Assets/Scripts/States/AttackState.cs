using UnityEngine;

public class AttackState : State
{
    public override void onEnter()
    {
        enemyAI.AttackTarget(1);
        Debug.Log("AttackState");
    }

    public override void onExit()
    {
        enemyAI.AttackTarget(0);
    }

    public override void onUpdate()
    {
        if(!enemyAI.targetInAttackRange())
        {
            fsm.SwitchState(typeof(ChaseState));
        }
    }

    public override int getNumber() { return 2; }
}
