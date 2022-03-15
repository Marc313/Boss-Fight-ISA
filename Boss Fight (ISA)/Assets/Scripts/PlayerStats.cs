using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void onDie()
    {
        Debug.Log("Dead");
    }
}
