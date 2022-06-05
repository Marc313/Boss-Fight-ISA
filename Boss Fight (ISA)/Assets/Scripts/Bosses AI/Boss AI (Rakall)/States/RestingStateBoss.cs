using UnityEngine;

public class RestingStateBoss : State
{
    public float AttackCooldown;
    private BossCombat bossCombat;

    private void Awake()
    {
        bossCombat = GetComponentInParent<BossCombat>();
    }

    public override void onEnter()
    {
        //bossCombat.StopAttackCombo();
        AttackCooldown = 3;
        bossAI.SetShieldMoveSpeed();
    }

    public override void onExit()
    {

    }

    public override void onUpdate()
    {
        RestingMovement();

        AttackCooldown -= Time.deltaTime;
        if(AttackCooldown <= 0)
        {
            fsm.SwitchState(typeof(ChaseStateBoss));
        }
    }

    private void RestingMovement()
    {
        bossAI.MoveAwayFromPlayer();

        bossAI.UpdateRunningValue();
    }
}