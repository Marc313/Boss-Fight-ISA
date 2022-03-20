using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Animator anim;

    private void Awake()
    {
        healthBar.Owner = this;
        anim = GetComponentInChildren<Animator>();
    }

    public override void onDie()
    {
        anim.SetTrigger("Death");
        GetComponent<EnemyAIFSM>().enabled = false;
        float animationDuration = anim.GetCurrentAnimatorClipInfo(0).Length;
        Invoke("destroyCorpse", animationDuration + .5f);
    }

    private void destroyCorpse()
    {
        Destroy(gameObject);
    }
}
