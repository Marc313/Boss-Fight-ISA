using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider Healthbar;
    private CharacterStats owner;

    public CharacterStats Owner { private get { return owner; } set { owner = value;  SubscribeFunction(); } }

    private void Awake()
    {
        Healthbar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        SubscribeFunction();
    }

    private void SubscribeFunction()
    {
        owner.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        owner.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float value)
    {
        if(value > 1) value = 1;
        if(value < 0) value = 0;

        Healthbar.value = value;
    }
}
