using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Transform hitbox;
    private Combat combat;
    private PlayerCombat playerCombat;      // Player Only

    private void Awake()
    {
        combat = GetComponentInParent<Combat>();
        playerCombat = (PlayerCombat) combat;
    }

    public void OverlapSphere(float damage)
    {
        combat.OverlapSphere(damage, hitbox);
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
}
