using UnityEngine;

public class AttackStateBoss : State
{
    public Attack attack;
    public AttackCombo attackCombo;
    private BossCombat bossCombat;

    private void Awake()
    {
        bossCombat = GetComponentInParent<BossCombat>();
    }

    public override void onEnter()
    {
        if (!bossAI.isInteracting && !bossAI.isInCombo)
        {
            bossAI.isInCombo = true;
            StartCoroutine(bossCombat.PerformAttackCombo(attackCombo));
        }
    }

    public override void onExit()
    {
        bossAI.isInCombo = false;
        bossCombat.StopAttackCombo(attackCombo);
    }

    public override void onUpdate()
    {
        if (!bossAI.targetInAttackRange() && !bossAI.isInteracting)
        {
            fsm.SwitchState(typeof(ChaseStateBoss));
            return;
        }
    }
}
