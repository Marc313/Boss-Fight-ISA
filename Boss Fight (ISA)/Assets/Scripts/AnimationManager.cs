using UnityEngine;

public static class AnimationManager
{
    public static void PlayAnimationClip(Animator anim, string animationName, bool isInteracting, float transitionTime = 0.2f)
    {
        anim.CrossFade(animationName, 0.2f);
    }
}