using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider Healthbar;

    private void Awake()
    {
        Healthbar = GetComponent<Slider>();
    }

    public void UpdateHealthBar(float value)
    {
        if(value > 1) value = 1;
        if(value < 0) value = 0;

        Healthbar.value = value;
    }
}
