using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { FIGHT, LOST, WON };

    public GameState state;
    public static event System.Action<GameState> OnStateChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = GameState.FIGHT;
    }

    public void ChangeState(GameState newState)
    {
        state = newState;
        OnStateChange?.Invoke(newState);

        if (newState != GameState.FIGHT)
        {
            Invoke(nameof(ReloadCurrentScene), 5f);
        }
    }

    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
