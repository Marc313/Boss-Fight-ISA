using UnityEngine;

public class CollisionBlocker : MonoBehaviour
{
    public Collider characterCollider;
    public Collider characterBlockerCollider;

    private void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }
}