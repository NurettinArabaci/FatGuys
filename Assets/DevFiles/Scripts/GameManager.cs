using UnityEngine;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    protected override void Awake()
    {
        base.Awake();
        GameStateEvent.OnChangeGameState += OnChangeGameState;
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
                HandleBegin();
                break;

            case GameState.Prepare:
                GameStateEvent.Fire_OnPrepareGame();
                break;

            case GameState.Play:
                GameStateEvent.Fire_OnPlayGame();
                break;

            default:
                break;
        }
    }



    public void HandleBegin()
    {

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
}

