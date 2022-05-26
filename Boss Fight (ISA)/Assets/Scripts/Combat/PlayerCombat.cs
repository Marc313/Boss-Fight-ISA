using UnityEngine;

public class PlayerCombat : Combat
{
    //public int numAttacks = 3;     // The number of the longest attack
    public bool IsBlocking { get; private set; }
    public bool IsShieldBashing { get; set; }

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
            movement.isInteracting = true;

            if(IsBlocking)
            {
                anim.SetTrigger("ShieldBash");
            }
            else
            {
                // SwordAttack
                /*if (currentAttack < numAttacks)*/ currentAttack++;
                PerformAttack(currentAttack, true);
            }
        }
    }

    private void HandleBlockInput()
    {
        if (!IsBlocking && !movement.isInteracting && Input.GetKey(KeyCode.Mouse1))
        {
            OnBlockStart();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            OnBlockOver();
        }
    }

    /*public void OnAttackOver(bool isInteracting)
    {
        playerMove.isInteracting = isInteracting;
        currentAttack = 0;
        SetAnimatorAttackValue(currentAttack);
    }*/

    #region MOVE TO COMBAT LATER, WHEN THERE ARE MANAGERS FOR PLAYER AND ENEMY
    public void OnBlockStart()
    {
        IsBlocking = true;
        anim.SetBool("IsBlocking", true);
        movement.SetShieldMoveSpeed();
    }

    public void OnBlockOver()
    {
        IsBlocking = false;
        anim.SetBool("IsBlocking", false);
        movement.SetRunMoveSpeed();
    }
    #endregion

    /*private void SetAnimatorAttackValue(int attack)
    {
        anim.SetInteger("Attack", attack);
    }*/
}
