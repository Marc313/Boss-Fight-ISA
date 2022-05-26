using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public float RunSpeed = 5f;
    public float ShieldWalkSpeed = 1f;

    public bool isInteracting;

    protected float currentMoveSpeed = 5f;
    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        if (GameManager.state != GameManager.GameState.FIGHT)
        {
            isInteracting = true;
        }
    }

    public void SetShieldMoveSpeed()
    {
        currentMoveSpeed = ShieldWalkSpeed;
    }

    public void SetRunMoveSpeed()
    {
        currentMoveSpeed = RunSpeed;
    }
}