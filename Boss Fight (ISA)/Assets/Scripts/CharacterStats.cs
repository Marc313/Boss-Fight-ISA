using System;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public static event Action<float> OnHealthChanged;
    public float MaxHealth = 100;
    protected float health;

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

        OnHealthChanged?.Invoke(GetHealthValue());
    }

    private float GetHealthValue()
    {
        return health / MaxHealth;
    }

    public abstract void onDie();
}
