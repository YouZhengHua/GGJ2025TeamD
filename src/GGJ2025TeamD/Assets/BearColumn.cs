using System;
using UnityEngine;

public class BearColumn : MonoBehaviour
{
    public Transform layer;
    [SerializeField]
    private AnimationCurve curve;

    private float flowValue =0f;
    [SerializeField]
    private float maxWidth = 0.3f;
    private bool isPlayingSound;
    private void Start()
    {
    }

    private void Update()
    {
        flowValue = curve.Evaluate(layer.eulerAngles.z);
        if (flowValue > 0 && !isPlayingSound)
        {
            AudioManager.PlaySound(SoundType.BEER_WATER);
            isPlayingSound = true;
        }
        if (flowValue <= 0)
        {
            AudioManager.StopSound(SoundType.BEER_WATER);
            isPlayingSound = false;
        }
        transform.localScale = new Vector3(maxWidth * flowValue, transform.localScale.y, transform.localScale.z);
        GlobalEvent.RaiseFlowUpdate(flowValue);
    }
}