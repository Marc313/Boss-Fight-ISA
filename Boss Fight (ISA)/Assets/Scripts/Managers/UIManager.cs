using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject HUDCanvas;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    
    private void Awake()
    {
        Instance = this;
    }

    public void enableHUDCanvas(bool active)
    {
        HUDCanvas.SetActive(active);
    }

    public void enableDeathScreen(bool active)
    {
        deathScreen.SetActive(active);
    }

    public void enableWinScreen(bool active)
    {
        winScreen.SetActive(active);
    }
}
