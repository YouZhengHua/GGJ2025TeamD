using System.Reflection;
using UnityEngine;

public class Bear : MonoBehaviour
{
    private Transform BearColumn;
    private Transform bear;
    private Transform Bubble;
    public float BearSpeed;
    public float BubbleSpeed;
    public float BubbleDissapearSpeed;
    public float offset;
    private bool isPlaying;

    [SerializeField]
    private float maxHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BearColumn = GameObject.FindGameObjectWithTag("BearColumn").transform;
        bear = gameObject.transform.GetChild(0);
        Bubble = gameObject.transform.GetChild(1);
        GlobalEvent.OnRoundReset += OnRoundReady;
        GlobalEvent.OnMouseUp += OnMouseUp;

    }

    private void OnRoundReady()
    {
        bear.localScale = new Vector3(1.332188f, 0, 1);
        Bubble.localScale = new Vector3(1.332188f, 0, 1);
        isBubbleOverHeight = false;
        isBearOverHeight = false;
    }

    private void OnMouseUp()
    {
        GlobalEvent.RaiseRoundEnd();
    }

    private void OnDestroy()
    {
        GlobalEvent.OnRoundReset -= OnRoundReady;
        GlobalEvent.OnMouseUp -= OnMouseUp;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(bear.transform.localScale.y/Bubble.localScale.y);
        // 倒酒的速率
        float pourRate = BearColumn.localScale.x;
        if (pourRate > 0 && !isPlaying)
        {
            AudioManager.PlaySound(SoundType.BEER);
            AudioManager.PlaySound(SoundType.BUBBLE);
            isPlaying = true;
        }
        bear.transform.localScale = new Vector3(bear.transform.localScale.x, 
            bear.transform.localScale.y + pourRate * BearSpeed *Time.deltaTime, 
            bear.transform.localScale.z);
        Bubble.localScale = new Vector3(Bubble.localScale.x,
            Bubble.localScale.y + pourRate * BearSpeed * BubbleSpeed *Time.deltaTime,
            Bubble.localScale.z);
        if (pourRate <= 0f)
        {
            isPlaying = false;
            AudioManager.StopSound(SoundType.BEER);
            float BubbleHeight = Bubble.localScale.y * Mathf.Exp(-BubbleDissapearSpeed * Time.deltaTime);
            if (BubbleHeight > bear.transform.localScale.y + offset)
            {
                AudioManager.StopSound(SoundType.BUBBLE);
                Bubble.localScale = new Vector3(Bubble.localScale.x,
                    BubbleHeight,
                    Bubble.localScale.z);
            }
        }

        if(Bubble.localScale.y > maxHeight)
        {
            Bubble.localScale = new Vector3(Bubble.localScale.x, maxHeight, Bubble.localScale.z);
            OnBubbleOverHeight();
        }

        if (bear.transform.localScale.y > maxHeight)
        {
            bear.transform.localScale = new Vector3(bear.transform.localScale.x, maxHeight, bear.transform.localScale.z);
            OnBearOverHeight();
        }
    }

    private bool isBubbleOverHeight = false;
    private bool isBearOverHeight = false;

    private void OnBubbleOverHeight()
    {
        if (isBubbleOverHeight)
            return;
        GlobalEvent.RaiseBubbleOverHeight();
    }

    private void OnBearOverHeight()
    {
        if (isBearOverHeight)
            return;
        GlobalEvent.RaiseBearOverHeight();
    }
}
