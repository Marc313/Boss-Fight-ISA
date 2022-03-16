using UnityEngine;

public class AttackState : State
{
    public override void onEnter()
    {
        enemyAI.AttackTarget(1, true);
        Debug.Log("AttackState");
    }

    public override void onExit()
    {
        enemyAI.AttackTarget(0, false);
    }

    public override void onUpdate()
    {
        if(!enemyAI.targetInAttackRange())
        {
            fsm.SwitchState(typeof(ChaseState));
            return;
        }

        if (enemyAI.currentAttack == 0) enemyAI.AttackTarget(1, true);
    }

    public override int getNumber() { return 2; }
}
