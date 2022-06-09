using System;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    public float MaxHealth = 100;
    protected float health;
    [SerializeField] protected HealthBar healthBar;
    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
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
            OnDie();
        }

        healthBar.UpdateHealthBar(GetHealthValue());
    }

    private float GetHealthValue()
    {
        return health / MaxHealth;
    }

    public abstract void OnDie();
}
