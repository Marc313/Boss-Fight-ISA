using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void OnDie()
    {
        anim.SetTrigger("Death");
        GameManager.Instance.ChangeState(GameManager.GameState.LOST);
    }
}
