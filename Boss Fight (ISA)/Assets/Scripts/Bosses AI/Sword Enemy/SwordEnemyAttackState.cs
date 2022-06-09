using UnityEngine;

public class SwordEnemyAttackState : State
{
    private SwordEnemyCombat enemyCombat;

    private void Awake()
    {
        enemyCombat = GetComponent<SwordEnemyCombat>();
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
            fsm.SwitchState(typeof(ChaseSwordEnemy));
            return;
        }

        if (enemyCombat.currentAttack == 0) enemyCombat.PerformAttack(1, true);
    }
}
