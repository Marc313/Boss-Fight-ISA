using UnityEngine;

public class IdleSwordEnemy : State
{
    public override void onEnter()
    {
        
    }

    public override void onExit()
    {
    }

    public override void onUpdate()
    {
        // Check player in range
        if (enemyAI.targetInSight())
        {
            fsm.SwitchState(typeof(ChaseSwordEnemy));
        }
    }
}
