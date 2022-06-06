using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void onDie()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.LOST);
    }
}
