using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void onDie()
    {
        GameManager.OnStateChange(GameManager.GameState.LOST);
    }
}
