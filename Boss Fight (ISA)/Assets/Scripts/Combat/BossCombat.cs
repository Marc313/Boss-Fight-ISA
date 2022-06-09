using System.Collections;
using UnityEngine;

public class BossCombat : Combat
{
    public BossAI bossAI;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<BossAI>();
        bossAI = (BossAI)movement;
    }

    public override void OverlapSphere(Attack attack, Transform hitbox)
    {
        Collider[] colliders = Physics.OverlapSphere(hitbox.position, 1f);

        foreach (Collider collider in colliders)
        {
            // Check for each collider if the collider is a player
            PlayerStats playerStats = collider.GetComponent<PlayerStats>();
            PlayerCombat playerCombat = collider.GetComponent<PlayerCombat>();

            if (playerCombat == null && playerStats == null) continue;

            if (playerCombat == null)
            {
                // No PlayerCombat? Take full damage.
                playerStats.takeDamage(attack.damage);
                break;
            }
            else if (playerStats == null)
            {
                if (attack.ignoresShieldBash) return;

                // No PlayerStats? Still Stagger
                if (playerCombat.IsShieldBashing)
                {
                    Stagger();
                    break;
                }
                continue;           // Continue to next target if the target was not shieldbashing
            }

            // If both are not null, check all options.
            if (!playerCombat.IsBlocking)
            {
                playerStats.takeDamage(attack.damage);
            }
            else if (playerCombat.IsBlocking && !playerCombat.IsShieldBashing)
            {
                playerStats.takeDamage(attack.damage / 2);      // Reduce damage by half when blocking.
            }
            else if (playerCombat.IsShieldBashing && !attack.ignoresShieldBash)
            {
                Stagger();
            }
            break;
        }
    }

    public void StopAttackCombo(AttackCombo combo)
    {
        StopCoroutine(PerformAttackCombo(combo));
        StopAllCoroutines();
        OnAttackOver();
    }

    public IEnumerator PerformAttackCombo(AttackCombo combo)
    {
        if (combo == null || combo.attacks == null) yield return null;

        foreach (Attack attack in combo.attacks)
        {
            // Perform attackk and wait until done
            PerformAttack(attack);
            float attackLength = attack.GetAnimationLength(anim);
            if (attackLength != -1)
            {
                yield return new WaitForSeconds(attackLength);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }

            // If still in attack state, perform next attack
            if (bossAI != null 
                && bossAI.targetInAttackRange() 
                && GameManager.Instance.state == GameManager.GameState.FIGHT)
            {
                continue;
            }
            else
            {
                break;
            }
        }

        if (bossAI != null)
        {
            bossAI.EnterRestingState(combo.AttackCooldown);
        }
    }
    

    public void PerformMagicAttack(MagicAttack magicAttack)
    {
        if (magicAttack == null) return;

        Vector3 bossPos = bossAI.transform.position;
        Transform playerTransform = bossAI.target;

        magicAttack.ShootAttackTowardsPlayer(bossPos, playerTransform);
    }
}