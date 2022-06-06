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
        /*if (anim.deltaPosition.magnitude > .2f)
        {
            movement.gameObject.transform.position += anim.deltaPosition;
        }*/

        //movement.gameObject.transform.position += anim.deltaPosition;

        if (movement.isInteracting || GameManager.Instance.state != GameManager.GameState.FIGHT)
        {
            movement.gameObject.transform.position += anim.deltaPosition * 2;
        }
    }
}