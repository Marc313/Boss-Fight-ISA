using System;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    public float MaxHealth = 100;
    protected float health;
    [SerializeField] protected HealthBar healthBar;

    private void Awake()
    {
    }

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
        health -= amount;
        if (health <= 0)
        {
            onDie();
        }

        healthBar.UpdateHealthBar(GetHealthValue());
    }

    private float GetHealthValue()
    {
        return health / MaxHealth;
    }

    public abstract void onDie();
}
