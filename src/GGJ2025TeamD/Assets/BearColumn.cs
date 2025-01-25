using System;
using UnityEngine;

public class BearColumn : MonoBehaviour
{
    public Transform layer;

    private void Start()
    {
    }

    private void Update()
    {
        float LayerAngle = layer.eulerAngles.z;
        transform.localScale = new Vector3(LayerAngle / 300, transform.localScale.y, transform.localScale.z);
    }
}