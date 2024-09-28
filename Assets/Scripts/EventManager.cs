using System;
using UnityEngine;

public static class EventManager
{
    private static GameState _state;
    public static GameState State
    {
        get => _state;
        set
        {
            _state = value;
            Debug.LogError(_state);
            EventManager.OnGameStateChanged?.Invoke(_state);
        }
    }

    public static event Action<GameState> OnGameStateChanged;
    public static event Action OnKeyObtained;
    public static event Action<float> OnCountdownStart;
}

public enum GameState
{
    Welcome,
    Start,
    Pause,
    Victory,
    Defeat,
    ClosseApp,
}