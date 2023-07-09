using System;

public class GameStateEvent
{
    public static event Action<GameState> OnChangeGameState;
    public static void Fire_OnChangeGameState(GameState gameState) { OnChangeGameState?.Invoke(gameState); }

    public static event Action OnPrepareGame;
    public static void Fire_OnPrepareGame() { OnPrepareGame?.Invoke(); }

    public static event Action OnPlayGame;
    public static void Fire_OnPlayGame() { OnPlayGame?.Invoke(); }


}
