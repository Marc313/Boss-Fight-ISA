using UnityEngine;

public class EnemyStats : CharacterStats
{
    public HealthBar healthbar;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public override void onDie()
    {
        anim.SetTrigger("Death");
        float animationDuration = anim.GetCurrentAnimatorClipInfo(0).Length;
        Invoke("destroyCorpse", animationDuration + .5f);
    }

    private void destroyCorpse()
    {
        Destroy(gameObject);
    }
}
