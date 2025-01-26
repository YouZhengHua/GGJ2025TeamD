using System;
using UnityEngine;
using UnityEngine.UI;

public class CupController : MonoBehaviour
{
    [SerializeField]
    private Transform zeroTransform;
    [SerializeField]
    private Transform overHeightTransform;
    [SerializeField]
    private Transform minTransform;
    [SerializeField]
    private Transform maxTransform;
    
    [SerializeField]
    private float totalAmount = 1000f;
    [SerializeField]
    private float bearAmount = 0f;
    [SerializeField]
    private float bubbleAmount = 0f;
    [SerializeField]
    private AnimationCurve bubbleCurve;
    
    [SerializeField]
    private GameObject bubbleFull;
    [SerializeField]
    private SpriteRenderer bearSR;
    [SerializeField]
    private SpriteRenderer bubbleSR;

    public void SetMaxAndMin(float max, float min)
    {
        float distance = overHeightTransform.position.y - zeroTransform.position.y;
        minTransform.localPosition = new Vector3(minTransform.localPosition.x, zeroTransform.localPosition.y + distance * min, minTransform.localPosition.z);
        maxTransform.localPosition = new Vector3(maxTransform.localPosition.x, zeroTransform.localPosition.y + distance * max, maxTransform.localPosition.z);
    }

    private void Update()
    {
        if (bubbleAmount > 0 && bubbleAmount > bearAmount * 0.1f)
        {
            bubbleAmount -= bubbleAmount * 0.1f * Time.deltaTime;
        }

        bearSR.material.SetFloat("_Amonut", bearAmount / totalAmount);
        bubbleSR.material.SetFloat("_Amonut", (bubbleAmount + bearAmount) / totalAmount);
    }

    public void AddAmount(float value, float flowRate)
    {
        bearAmount += value;
        bubbleAmount += value * bubbleCurve.Evaluate(flowRate);
        if (bearAmount > totalAmount)
        {
            GlobalEvent.RaiseBearOverHeight();
            bubbleFull.SetActive(true);
        }
        
        if (bearAmount + bubbleAmount > totalAmount)
        {
            GlobalEvent.RaiseBubbleOverHeight();
            bubbleFull.SetActive(true);
        }
    }

    public void Init()
    {
        bubbleAmount = 0f;
        bearAmount = 0f;
        bubbleFull.SetActive(false);
        
        bearSR.material.SetFloat("_Amonut", 0);
        bubbleSR.material.SetFloat("_Amonut", 0);
    }

    public float AmountRate => bearAmount / totalAmount;
}
