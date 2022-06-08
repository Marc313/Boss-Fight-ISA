using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Attack")]
public class Attack : ScriptableObject
{
    public AnimationClip AttackClip;
    public float damage;
    public bool ignoresShieldBash;
    public bool staggersPlayer;
    public bool useRootMotion;

    public void PerformAttack(Animator ownerAnimator)
    {
        ownerAnimator.applyRootMotion = useRootMotion;
        AnimationManager.PlayAnimationClip(ownerAnimator, AttackClip.name, true);
    }

    public float GetAnimationLength(Animator ownerAnimator)
    {
        RuntimeAnimatorController controller = ownerAnimator.runtimeAnimatorController;
        foreach(AnimationClip clip in controller.animationClips)
        {
            if(clip.name.Equals(AttackClip.name))
            {
                return clip.length;
            }
        }
        return -1;
    }
}