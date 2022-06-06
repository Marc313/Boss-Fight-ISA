using UnityEngine;

public class ChaseStateBoss : State
{
    private float chaseTime;

    public override void onEnter()
    {
        chaseTime = 0;
        bossAI.continueChase();
    }

    public override void onExit()
    {
        bossAI.stopChase();
    }

    public override void onUpdate()
    {
        chaseTime += Time.deltaTime;

        if (bossAI.isInteracting)
        {
            bossAI.stopChase();
        }
        else
        {
            bossAI.continueChase();
            bossAI.chasePlayer();
        }


        // If the chase is going on too long, do a ranged attack.
        if (bossAI.targetInSight() && chaseTime > 4f)
        {
            fsm.SwitchState(typeof(RangedAttackStateBoss));
            return;
        }

        if (bossAI.targetInAttackRange())
        {
            fsm.SwitchState(typeof(AttackStateBoss));
            return;
        }

        bossAI.UpdateRunningValue();
    }
}