using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(TMP_Text))]
public class ResultText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text? text;
    
    private void Start()
    {
        GlobalEvent.OnRoundSuccess += OnRoundSuccess;
        GlobalEvent.OnRoundFail += OnRoundFail;
    }

    private void OnDestroy()
    {
        GlobalEvent.OnRoundSuccess -= OnRoundSuccess;
        GlobalEvent.OnRoundFail -= OnRoundFail;
    }

    private void OnRoundSuccess()
    {
        if (text != null)
        {
            text.text = "Success";
        }
    }

    private void OnRoundFail()
    {
        if (text != null)
        {
            text.text = "Fail";
        }
    }
}
