using UnityEngine;

public class AttackStateBoss : State
{
    //public Attack attack;
    public AttackCombo[] MeleeAttackCombos;

    private AttackCombo CurrentAttackCombo;
    private BossCombat bossCombat;

    private void Awake()
    {
        bossCombat = GetComponentInParent<BossCombat>();
    }

    public override void onEnter()
    {
        // Choose an attack.
        int randomAttackIndex = Random.Range(0, MeleeAttackCombos.Length);
        CurrentAttackCombo = MeleeAttackCombos[randomAttackIndex];

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
        if (!bossAI.targetInAttackRange() && !bossAI.isInteracting)
        {
            fsm.SwitchState(typeof(ChaseStateBoss));
            return;
        }
    }
}
