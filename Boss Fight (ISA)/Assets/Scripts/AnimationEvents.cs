using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void EnableSwordCollider()
    {
        if (GetComponentInChildren<DamageCollider>() == null) Debug.Log("'_'");
        GetComponentInChildren<DamageCollider>()?.EnableCollider();
    }

    public void DisableSwordCollider()
    {
        GetComponentInChildren<DamageCollider>()?.DisableCollider();
    }
}
