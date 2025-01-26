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
    private void Start()
    {
    }

    private void Update()
    {
        flowValue = curve.Evaluate(layer.eulerAngles.z);
        transform.localScale = new Vector3(maxWidth * flowValue, transform.localScale.y, transform.localScale.z);
        GlobalEvent.RaiseFlowUpdate(flowValue);
    }
}