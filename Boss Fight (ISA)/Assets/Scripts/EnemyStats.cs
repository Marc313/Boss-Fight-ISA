using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float MaxHealth = 100;
    private float health;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        health = MaxHealth;
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > MaxHealth) health = MaxHealth;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            onDie();
        }
    }

    public void onDie()
    {
        anim.SetBool("Dead", true);
        float animationDuration = anim.GetCurrentAnimatorClipInfo(0).Length;
        Invoke("destroyCorpse", animationDuration + .5f);
    }

    private void destroyCorpse()
    {
        Destroy(gameObject);
    }
}
