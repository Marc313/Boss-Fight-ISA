using UnityEngine;

public class ApplyRootMotionToParent : MonoBehaviour
{
    private Animator anim;
    private Movement movement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponentInParent<Movement>();
    }

    private void OnAnimatorMove()
    {
        if (movement.isInteracting || GameManager.Instance.state != GameManager.GameState.FIGHT)
        {
            Vector3 rootMovement = anim.deltaPosition * 2;
            rootMovement.y = 0;
            movement.gameObject.transform.position += rootMovement;
        }
    }
}