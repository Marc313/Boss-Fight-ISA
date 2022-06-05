using UnityEngine;

public class RestingStateBoss : State
{
    public float AttackCooldown;

    public override void onEnter()
    {
        AttackCooldown = 3;
        bossAI.SetShieldMoveSpeed();
        bossAI.EnableAgentRotation(false);

        StartRestingMovement();
    }

    public override void onExit()
    {
        bossAI.EnableAgentRotation(true);

        CancelInvoke(nameof(UpdateRestingMovement));
    }

    public override void onUpdate()
    {
        AttackCooldown -= Time.deltaTime;
        if (AttackCooldown <= 0)
        {
            if (bossAI.targetInAttackRange())
            {
                fsm.SwitchState(typeof(AttackStateBoss));
            }
            else
            {
                fsm.SwitchState(typeof(ChaseStateBoss));
            }
        }
    }

    private void StartRestingMovement()
    {
        InvokeRepeating(nameof(UpdateRestingMovement), 0f, .5f);
    }

    private void UpdateRestingMovement()
    {
        bossAI.MoveAwayFromPlayer();
        bossAI.UpdateRunningValue();
    }
}