using UnityEngine;

public class ChaseStateBoss : State
{
    public override void onEnter()
    {
        bossAI.continueChase();
    }

    public override void onExit()
    {
        bossAI.stopChase();
    }

    public override void onUpdate()
    {
        /*if (bossAI.targetOutOfSight())
        {
            fsm.SwitchState(typeof(IdleSwordEnemy));
            return;
        }*/

        if (bossAI.targetInAttackRange())
        {
            fsm.SwitchState(typeof(AttackStateBoss));
            //Debug.Log("Switch!");
            return;
        }

        if (bossAI.isInteracting)
        {
            //Debug.Log("Stop");
            bossAI.stopChase();
        }
        else
        {
            //Debug.Log("Continue");
            bossAI.continueChase();
            bossAI.chasePlayer();
        }

        bossAI.UpdateRunningValue();
    }
}