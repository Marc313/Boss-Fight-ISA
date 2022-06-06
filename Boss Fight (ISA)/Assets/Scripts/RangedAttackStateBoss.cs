using UnityEngine;

public class RangedAttackStateBoss : State
{
    public AttackCombo[] RangedAttacks;
    private AttackCombo CurrentAttackCombo;
    private BossCombat bossCombat;

    private void Awake()
    {
        bossCombat = GetComponentInParent<BossCombat>();
    }

    public override void onEnter()
    {
        CurrentAttackCombo = RangedAttacks[0];

        if (!bossAI.isInteracting && !bossAI.isInCombo)
        {
            bossAI.isInCombo = true;
            StartCoroutine(bossCombat.PerformAttackCombo(CurrentAttackCombo));
        }
    }

    public override void onExit()
    {
        bossAI.isInCombo = false;
        bossCombat.StopAttackCombo(CurrentAttackCombo);
    }

    public override void onUpdate()
    {
        if (!bossAI.isInteracting)
        {
            fsm.SwitchState(typeof(ChaseStateBoss));
            return;
        }
    }


}