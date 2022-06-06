using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject HUDCanvas;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnStateChange += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnStateChange -= OnGameStateChanged;
    }

    public void OnGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.FIGHT:
                enableHUDCanvas(true);
                enableDeathScreen(false);
                enableWinScreen(false);
                break;
            case GameManager.GameState.LOST:
                enableHUDCanvas(false);
                enableDeathScreen(true);
                break;
            case GameManager.GameState.WON:
                enableHUDCanvas(false);
                enableWinScreen(true);
                break;
        }
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
