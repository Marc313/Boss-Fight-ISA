using UnityEngine;

public class AttackState : State
{
    private EnemyCombat enemyCombat;

    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
    }

    public override void onEnter()
    {
        enemyCombat.PerformAttack(1, true);
    }

    public override void onExit()
    {
        enemyCombat.PerformAttack(0, false);
    }

    public override void onUpdate()
    {
        if(!enemyAI.targetInAttackRange())
        {
            fsm.SwitchState(typeof(ChaseState));
            return;
        }

        if (enemyCombat.currentAttack == 0) enemyCombat.PerformAttack(1, true);
    }
}
