using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Attack Combo")]
public class AttackCombo : ScriptableObject
{
    public Attack[] attacks;
    public float AttackCooldown;     // Amount of time in seconds AFTER attack that the AI will not attack
    public int priority;

}