using UnityEngine;

public class ApplyRootMotionToParent : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement movement;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void OnAnimatorMove()
    {
        Debug.Log("OnAnimatorMove: " + anim.deltaPosition.ToString());
        /*if (anim.deltaPosition.magnitude > .2f)
        {
            movement.gameObject.transform.position += anim.deltaPosition;
        }*/

        if (movement.isDodging)
        {
            movement.gameObject.transform.position += anim.deltaPosition * 2;
        }
    }
}