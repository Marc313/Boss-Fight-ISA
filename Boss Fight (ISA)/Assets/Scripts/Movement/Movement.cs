using System.Collections;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    #region Variables
    public float RunSpeed = 5f;
    public float ShieldWalkSpeed = 1f;
    public float rotationSpeed;

    public bool isInteracting;

    protected float currentMoveSpeed = 5f;
    protected Animator anim;
    #endregion

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.FIGHT)
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

    /**
     * Rotates the object from one orientation to another with a certain rotation speed.
     */
    public IEnumerator RotateTowardsSlowly(Quaternion oldRotation, Quaternion targetRotation, System.Action onDone = null)
    {
        //Debug.Log("Coroutine Start");
        float angle = Quaternion.Angle(oldRotation, targetRotation);
        float time = angle / rotationSpeed;
        float t = 0;

        transform.rotation = oldRotation;

        while (t < time)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;

        onDone?.Invoke();
        //Debug.Log("Coroutine End");
    }
}