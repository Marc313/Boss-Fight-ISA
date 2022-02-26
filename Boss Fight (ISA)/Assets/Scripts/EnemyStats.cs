using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float MaxHealth = 100;
    private float health;

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
        Debug.Log("AUU!");
        health -= amount;
        if (health <= 0)
        {
            onDie();
        }
    }

    public void onDie()
    {
        Debug.Log("Dood enzo");
        Destroy(gameObject);
    }
}
