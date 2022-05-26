using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyAIFSM movement;
    public EnemyCombat combat;

    public bool IsInteracting;
}