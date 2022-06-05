using UnityEngine;

public class ResponseAnimator : StateMachineBehaviour
{
    private PlayerCombat playerCombat;
    private SwordEnemyAI enemy;

    private void Awake()
    {
        // playerCombat = FindObjectOfType<PlayerCombat>();
        // enemy = FindObjectOfType<EnemyAIFSM>();
    }

    public void onPlayerAttackOver(bool isInteracting)
    {
        FindObjectOfType<PlayerCombat>()?.OnAttackOver(isInteracting);
    }

    public void OnPlayerBlockOver()
    {
        FindObjectOfType<PlayerCombat>()?.OnBlockOver();
    }

    public void OnPlayerDodgeOver()
    {
        FindObjectOfType<PlayerMovement>()?.OnDodgeOver();
    }

    public void OnEnemyAttackOver(bool isInteracting)
    {
        FindObjectOfType<BossCombat>()?.OnAttackOver(isInteracting);
        FindObjectOfType<SwordEnemyCombat>()?.OnAttackOver(isInteracting);
    }

    public void EnemyPerformNextAttack()
    {
        FindObjectOfType<SwordEnemyCombat>()?.PerformNextAttack();
    }

    public void onEnemyStaggerOver()
    {
        FindObjectOfType<BossCombat>()?.OnStaggerOver();
        FindObjectOfType<SwordEnemyCombat>()?.OnStaggerOver();
    }
}
