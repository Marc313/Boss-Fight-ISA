using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Transform hitbox;
    private Combat combat;
    private PlayerCombat playerCombat;      // Player Only
    private BossCombat bossCombat;

    private void Awake()
    {
        combat = GetComponentInParent<Combat>();
        if (combat.GetType() == typeof(PlayerCombat))
        {
            playerCombat = (PlayerCombat) combat;
        } 
        else if (combat.GetType() == typeof(BossCombat))
        {
            bossCombat = (BossCombat)combat;
        }
    }

    public void OverlapSphere(Attack attack)
    {
        combat.OverlapSphere(attack, hitbox);
    }

    #region Player-Only
    public void ShieldBashStart()
    {
        if (playerCombat != null)
        playerCombat.IsShieldBashing = true;
    }

    public void ShieldBashEnd()
    {
        if (playerCombat != null)
        playerCombat.IsShieldBashing = false;
    }
    #endregion

    public void ShootProjectile(MagicAttack magicAttack)
    {
        bossCombat.PerformMagicAttack(magicAttack);
    }
}
