using UnityEngine;

public class CombatOneHanded : MonoBehaviour
{
    public int numAttacks = 3;     // The number of the longest 

    private Animator anim;
    private int currentAttack;
    private PlayerMovement playerMove;
    private bool IsBlocking;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerMove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttackInput();
        HandleBlockInput();
    }

    private void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerMove.isInteracting = true;
            if (currentAttack < numAttacks) currentAttack++;
            SetAnimatorAttackValue(currentAttack);
        }
    }

    private void HandleBlockInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerMove.isInteracting = true;
            IsBlocking = true;
            anim.SetBool("IsBlocking", true);
        }
    }

    public void OnBlockOver()
    {
        playerMove.isInteracting = false;
        IsBlocking = false;
        anim.SetBool("IsBlocking", false);
    }

    public void OnAttackOver(bool isInteracting)
    {
        playerMove.isInteracting = isInteracting;
        currentAttack = 0;
        SetAnimatorAttackValue(currentAttack);
    }

    private void SetAnimatorAttackValue(int attack)
    {
        anim.SetInteger("Attack", attack);
    }
}
