using UnityEngine;

public class IdleState : State
{
    public override void onEnter()
    {
        enemyAI.returnToPosition();
    }

    public override void onExit()
    {
    }

    public override void onUpdate()
    {
        // Check player in range
        if (enemyAI.targetInSight())
        {
            fsm.SwitchState(typeof(ChaseState));
        }
    }

    public override int getNumber() { return 0; }
}
