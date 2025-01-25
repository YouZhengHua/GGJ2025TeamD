using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundController : MonoBehaviour
{
    private int minValue = 50;
    private int maxValue = 100;
    
    private int nowScore = 0;
    
    [SerializeField]
    private int passMinAmount = 0;
    [SerializeField]
    private int passMaxAmonut = 100;
    [SerializeField]
    private int bonusCount = 0;

    private void Awake()
    {
        GlobalEvent.OnRoundEnd += OnRoundEnd;
        GlobalEvent.OnRoundStart += RoundStart;
    }

    private void Start()
    {

        nowScore = 0;
        bonusCount = 0;
    }

    private void OnDestroy()
    {
        GlobalEvent.OnRoundStart -= RoundStart;
        GlobalEvent.OnRoundEnd -= OnRoundEnd;
    }

    /// <summary>
    /// 生成飲料杯
    /// 生成合格標準
    /// </summary>
    private void RoundStart()
    {
        passMinAmount = Random.Range(minValue, maxValue - 10);
        passMaxAmonut = passMinAmount + 10;
    }

    private void OnRoundEnd(float value)
    {
        if (value >= passMinAmount && value <= passMaxAmonut)
        {
            bonusCount += 1;
            
            nowScore += 100;
            
            GlobalEvent.RaiseScoreChange(nowScore);
            GlobalEvent.RaiseRoundSuccess();
        }
        else
        {
            bonusCount = 0;
            GlobalEvent.RaiseRoundFail();
        }
        
        GlobalEvent.RaiseGameStart();
    }
    
    public int GetMinAmount => passMinAmount;
    public int GetMaxAmount => passMaxAmonut;
}
