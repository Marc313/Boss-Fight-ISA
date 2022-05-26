using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { FIGHT, LOST, WON };

    public static GameState state;
    //public static GameState gameState { get { return state; } set { OnStateChange(value); } }

    public static void OnStateChange(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.FIGHT:
                UIManager.Instance.enableHUDCanvas(true);
                UIManager.Instance.enableDeathScreen(false);
                UIManager.Instance.enableWinScreen(false);
                break;
            case GameState.LOST:
                UIManager.Instance.enableHUDCanvas(false);
                UIManager.Instance.enableDeathScreen(true);
                break;
            case GameState.WON:
                UIManager.Instance.enableHUDCanvas(false);
                UIManager.Instance.enableWinScreen(true);
                break;
        }
    }

}
