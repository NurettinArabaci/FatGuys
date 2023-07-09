using UnityEngine;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    protected override void Awake()
    {
        base.Awake();
        GameStateEvent.OnChangeGameState += OnChangeGameState;

        DOTween.SetTweensCapacity(200000, 200);
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

            case GameState.Play:
                HandlePlay();
                break;

            case GameState.PopUp:
                HandlePopUp();
                break;

            default:
                break;
        }
    }



    public void HandleBegin()
    {
        UIManager.Instance.StartPanelActive(true);

    }

    public void HandlePlay()
    {
        Time.timeScale = 1;
        UIManager.Instance.StartPanelActive(false);
    }

    public void HandlePopUp()
    {
        Time.timeScale = 0;
    }


    private void OnDisable()
    {
        GameStateEvent.OnChangeGameState -= OnChangeGameState;
    }
}


public enum GameState
{
    Begin,
    Play,
    PopUp
}

