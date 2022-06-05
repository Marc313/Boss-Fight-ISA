using UnityEngine;

public abstract class Combat : MonoBehaviour
{
    public int currentAttack { get; protected set; }

    protected Animator anim;
    protected Movement movement;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        movement = GetComponent<Movement>();
    }

    public void PerformAttack(int attackID, bool isInteracting)
    {
        movement.isInteracting = isInteracting;
        currentAttack = attackID;
        anim.SetInteger("Attack", currentAttack);
    }

    public virtual void OverlapSphere(Attack attack, Transform hitbox)
    {
        Collider[] colliders = Physics.OverlapSphere(hitbox.position, 1f);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == this.gameObject) continue; // If a character hits itself, ignore

            CharacterStats characterStats = collider.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                characterStats.takeDamage(attack.damage);
                break;
            }
        }
    }

    public void Stagger()
    {
        movement.isInteracting = true;
        currentAttack = 0;
        anim.SetBool("IsStaggering", true);
    }

    public void OnStaggerOver()
    {
        movement.isInteracting = false;
        anim.SetBool("IsStaggering", false);
    }

    public void OnAttackOver(bool isInteracting = false)
    {
        currentAttack = 0;
        anim.SetInteger("Attack", currentAttack);
        movement.isInteracting = isInteracting;
    }
}