using System;
using UnityEngine;

public class BearColumn : MonoBehaviour
{
    public Transform layer;
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float flowRate = 500f;
    [SerializeField]
    private float maxWidth = 0.3f;
    [SerializeField]
    private RoundController roundController;
    private bool isPlayingSound;

    private void Update()
    {
        float flowValue = curve.Evaluate(layer.eulerAngles.z);
        if (flowValue > 0f)
        {
            transform.localScale = new Vector3(maxWidth * flowValue, transform.localScale.y, transform.localScale.z);
            if (roundController.NowCup != null)
            {
                roundController.NowCup.AddAmount(flowValue * flowRate * Time.deltaTime, flowRate);
            }

            if (!isPlayingSound)
            {
                AudioManager.PlaySound(SoundType.BEER_WATER);
                isPlayingSound = true;
            }
        }
        else
        {
            transform.localScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);
            AudioManager.StopSound(SoundType.BEER_WATER);
            isPlayingSound = false;
        }
    }
}