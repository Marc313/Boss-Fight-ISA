using UnityEngine;

public class CombatOneHanded : MonoBehaviour
{
    public int numAttacks = 3;     // The number of the longest 

    private Animator anim;
    private int currentAttack;
    private PlayerMovement playerMove;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerMove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerMove.isInteracting = true;
            if (currentAttack < numAttacks) currentAttack++;
            setAnimatorAttackValue(currentAttack);
        }
    }

    public void onAttackOver(bool isInteracting)
    {
        playerMove.isInteracting = isInteracting;
        currentAttack = 0;
        setAnimatorAttackValue(currentAttack);
    }

    private void setAnimatorAttackValue(int attack)
    {
        anim.SetInteger("Attack", attack);
    }
}
