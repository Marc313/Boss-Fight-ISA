using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxes : MonoBehaviour
{
    public Transform hitbox;

    public void OverlapSphere()
    {
        Collider[] colliders = Physics.OverlapSphere(hitbox.position, 1f);

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
            PlayerStats playerStats = collider.GetComponent<PlayerStats>();

            if(playerStats != null)
            {
                playerStats.takeDamage(10f);
                Debug.Log("AU");
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitbox.position, 1f);
    }
}
