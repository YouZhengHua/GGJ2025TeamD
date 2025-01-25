using UnityEngine;
using System;

public class GlobalEvent
{
    /// <summary>
    /// 遊戲開始
    /// </summary>
    public static event Action? OnGameStart;

    public static void RaiseGameStart()
    {
        OnGameStart?.Invoke();
    }
    
    /// <summary>
    /// 倒飲料開始
    /// </summary>
    public static event Action? OnRoundStart;
    public static void RaiseRoundStart()
    {
        OnRoundStart?.Invoke();
    }
    
    /// <summary>
    /// 倒飲料結束
    /// </summary>
    public static event Action<float>? OnRoundEnd;

    public static void RaiseRoundEnd(float value)
    {
        OnRoundEnd?.Invoke(value);
    }
    
    public static event Action? OnRoundSuccess;
    public static void RaiseRoundSuccess()
    {
        OnRoundSuccess?.Invoke();
    }
    
    public static event Action? OnRoundFail;
    public static void RaiseRoundFail()
    {
        OnRoundFail?.Invoke();
    }
    
    /// <summary>
    /// 遊戲結束
    /// </summary>
    public static event Action? OnGameEnd;
    public static void RaiseGameEnd()
    {
        OnGameEnd?.Invoke();
    }
    
    public static event Action<int>? OnScoreChange;
    public static void RaiseScoreChange(int score)
    {
        OnScoreChange?.Invoke(score);
    }
}
