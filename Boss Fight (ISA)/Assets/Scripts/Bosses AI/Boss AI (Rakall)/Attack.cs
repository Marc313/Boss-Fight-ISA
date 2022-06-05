using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Attack")]
public class Attack : ScriptableObject
{
    public string AnimationName;
    public float damage;
    public bool ignoresShieldBash;
    public bool staggersPlayer;

    public void PerformAttack(Animator ownerAnimator)
    {
        AnimationManager.PlayAnimationClip(ownerAnimator, AnimationName, true);
    }

    public float GetAnimationLength(Animator ownerAnimator)
    {
        RuntimeAnimatorController controller = ownerAnimator.runtimeAnimatorController;
        foreach(AnimationClip clip in controller.animationClips)
        {
            if(clip.name.Equals(AnimationName))
            {
                return clip.length;
            }
        }
        return -1;
    }
}