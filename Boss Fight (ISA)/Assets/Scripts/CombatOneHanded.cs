using UnityEngine;

public class CombatOneHanded : MonoBehaviour
{
    public int numAttacks = 3;     // The number of the longest attack
    public int currentAttack { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool IsShieldBashing { get; set; }

    private Animator anim;
    private PlayerMovement playerMove;

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

            if(IsBlocking)
            {
                // ShieldBash
                //IsShieldBashing = true;
                anim.SetTrigger("ShieldBash");
            }
            else
            {
                // SwordAttack
                if (currentAttack < numAttacks) currentAttack++;
                SetAnimatorAttackValue(currentAttack);
            }
        }
    }

    private void HandleBlockInput()
    {
        if (!playerMove.isInteracting && Input.GetKey(KeyCode.Mouse1))
        {
            IsBlocking = true;
            anim.SetBool("IsBlocking", true);
            playerMove.SetShieldMoveSpeed();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            OnBlockOver();
        }
    }

    public void OnAttackOver(bool isInteracting)
    {
        playerMove.isInteracting = isInteracting;
        currentAttack = 0;
        SetAnimatorAttackValue(currentAttack);
    }

    public void OnBlockOver()
    {
        IsBlocking = false;
        anim.SetBool("IsBlocking", false);
        playerMove.SetRunMoveSpeed();
    }

    public void OnShieldBashOver()
    {
        playerMove.isInteracting = false;
        //IsShieldBashing = false;
        anim.SetBool("ShieldBash", false);
    }

    private void SetAnimatorAttackValue(int attack)
    {
        anim.SetInteger("Attack", attack);
    }
}
