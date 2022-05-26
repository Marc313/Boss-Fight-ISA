using UnityEngine;

public class ResponseAnimator : StateMachineBehaviour
{
    private CombatOneHanded playerCombat;
    private EnemyAIFSM enemy;

    private void Awake()
    {
        playerCombat = FindObjectOfType<CombatOneHanded>();
        enemy = FindObjectOfType<EnemyAIFSM>();
    }

    public void onPlayerAttackOver(bool isInteracting)
    {
        FindObjectOfType<CombatOneHanded>()?.OnAttackOver(isInteracting);
    }

    public void OnPlayerBlockOver()
    {
        FindObjectOfType<CombatOneHanded>()?.OnBlockOver();
    }

    public void OnPlayerShieldBashOver()
    {
        FindObjectOfType<CombatOneHanded>()?.OnShieldBashOver();
    }

    public void onEnemyAttackOverMovement(int attackID)
    {
        FindObjectOfType<EnemyAIFSM>().AttackTarget(attackID, false);
    }

    public void onEnemyAttackOverInteracting(int attackID)
    {
        FindObjectOfType<EnemyAIFSM>().AttackTarget(attackID, true);
    }

    public void onEnemyStaggerOver()
    {
        FindObjectOfType<EnemyAIFSM>().OnStaggerOver();
    }

    public void onEnemyAttackOver()
    {
        EnemyAIFSM enemy = FindObjectOfType<EnemyAIFSM>();

        if (enemy.targetInAttackRange())
        {
            int attackID = enemy.currentAttack == 2 ? 2 : enemy.currentAttack + 1;
            enemy.AttackTarget(attackID, true);
        }
        else
        {
            enemy.AttackTarget(0, false);
        }
    }
}
