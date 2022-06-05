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
        //bossCombat.PerformAttack(attack);
    }

    public override void onExit()
    {
        bossCombat.StopAttackCombo(attackCombo);
    }

    public override void onUpdate()
    {
        Debug.Log("AttackState");
        if (!bossAI.targetInAttackRange())
        {
            Debug.Log("Leaving Attackstate");
            fsm.SwitchState(typeof(ChaseStateBoss));
            return;
        }

        if (!bossAI.isInteracting)
        {
            StartCoroutine(bossCombat.PerformAttackCombo(attackCombo));
        }
    }
}
