using UnityEngine;

public class ResponseAnimator : StateMachineBehaviour
{
    private PlayerCombat playerCombat;
    private EnemyAIFSM enemy;

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
        FindObjectOfType<EnemyCombat>().OnAttackOver(isInteracting);
    }

    public void EnemyPerformNextAttack()
    {
        FindObjectOfType<EnemyCombat>().PerformNextAttack();
    }

    public void onEnemyStaggerOver()
    {
        FindObjectOfType<EnemyCombat>().OnStaggerOver();
    }
}
