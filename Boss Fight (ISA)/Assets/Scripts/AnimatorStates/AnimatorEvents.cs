using UnityEngine;
using UnityEngine.Events;

public class AnimatorEvents : StateMachineBehaviour
{
    public UnityEvent onStateEnter;
    public UnityEvent onStateUpdate;
    public UnityEvent onStateExit;
    public UnityEvent onStateMove;
    public UnityEvent onStateIK;

    // onstateenter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        onStateEnter?.Invoke();
    }

    // onstateupdate is called on each update frame between onstateenter and onstateexit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        onStateUpdate?.Invoke();
    }

    // onstateexit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        onStateExit?.Invoke();
    }

    // onstatemove is called right after animator.onanimatormove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // implement code that processes and affects root motion
        onStateMove?.Invoke();
    }

    // onstateik is called right after animator.onanimatorik()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // implement code that sets up animation ik (inverse kinematics)
        onStateIK?.Invoke();
    }
}
