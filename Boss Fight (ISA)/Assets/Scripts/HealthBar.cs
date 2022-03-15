using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider Healthbar;
    public CharacterStats Owner { private get; set; }

    private void Awake()
    {
        Healthbar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        EnemyStats.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        EnemyStats.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float value)
    {
        if(value > 1) value = 1;
        if(value < 0) value = 0;

        Healthbar.value = value;
    }
}
