using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(TMP_Text))]
public class TimeText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text? timeText;


    private void Update()
    {
        if (timeText != null)
        {
            timeText.text = GameManager.Instance.NowTime.ToString("f0");
        }
    }
}
