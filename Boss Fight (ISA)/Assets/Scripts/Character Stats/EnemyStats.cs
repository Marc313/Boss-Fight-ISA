using UnityEngine;

public class EnemyStats : CharacterStats
{
    private bool Died;

    /*public override void onDie()
    {
        if (Died) return;

        Died = true;
        anim.SetTrigger("Death");
        //GetComponent<SwordEnemyAI>().enabled = false;
        GetComponent<BossAI>().enabled = false;
        float animationDuration = anim.GetCurrentAnimatorClipInfo(0).Length;
        Invoke("destroyCorpse", animationDuration + .5f);
    }

    private void destroyCorpse()
    {
        Destroy(gameObject);
        GameManager.Instance.ChangeState(GameManager.GameState.WON);
    }*/

    public override void OnDie()
    {
        Died = true;
        anim.SetTrigger("Death");
        GameManager.Instance.ChangeState(GameManager.GameState.WON);
    }
}
