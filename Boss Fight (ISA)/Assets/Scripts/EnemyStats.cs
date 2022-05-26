using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Animator anim;
    private bool Died;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public override void onDie()
    {
        if (Died) return;

        Died = true;
        anim.SetTrigger("Death");
        GetComponent<EnemyAIFSM>().enabled = false;
        float animationDuration = anim.GetCurrentAnimatorClipInfo(0).Length;
        Invoke("destroyCorpse", animationDuration + .5f);
    }

    private void destroyCorpse()
    {
        Destroy(gameObject);
        GameManager.OnStateChange(GameManager.GameState.WON);
    }
}
