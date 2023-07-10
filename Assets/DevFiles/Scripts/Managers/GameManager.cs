using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    [SerializeField] private float _countDownTime = 90;
    public float CountDownTime => _countDownTime;

    protected override void Awake()
    {
        base.Awake();
        GameStateEvent.OnChangeGameState += OnChangeGameState;
    }

    IEnumerator CountDown()
    {
        while(CountDownTime>0)
        {
            _countDownTime -= Time.deltaTime;
            yield return null;
        }

        OnChangeGameState(GameState.Lose);
    }

    private void Start()
    {
        OnChangeGameState(GameState.Begin);
    }

    void OnChangeGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Begin:
                GameStateEvent.Fire_OnBeginGame();
                break;

            case GameState.Prepare:
                GameStateEvent.Fire_OnPrepareGame();
                break;

            case GameState.Play:
                GameStateEvent.Fire_OnPlayGame();
                StartCoroutine(CountDown());
                break;

            case GameState.Win:
                GameStateEvent.Fire_OnWinGame();
                break;

            case GameState.Lose:
                GameStateEvent.Fire_OnLoseGamee();
                break;

            default:
                break;
        }
    }



    private void OnDisable()
    {
        GameStateEvent.OnChangeGameState -= OnChangeGameState;
    }
}


public enum GameState
{
    Begin,
    Prepare,
    Play,
    Win,
    Lose
}

