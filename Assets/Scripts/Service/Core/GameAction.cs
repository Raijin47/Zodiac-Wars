using System;
using UnityEngine.Events;

[Serializable]
public class GameAction
{
    public UnityEvent OnStartGame;
    public UnityEvent OnGameOver;
    public UnityEvent OnWin;

    public void SendStartGame() => OnStartGame?.Invoke();
    public void SendGameOver() => OnGameOver?.Invoke();
    public void SendWin() => OnWin?.Invoke();
}