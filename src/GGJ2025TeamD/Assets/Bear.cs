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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BearColumn = GameObject.FindGameObjectWithTag("BearColumn").transform;
        bear = gameObject.transform.GetChild(0);
        Bubble = gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(bear.transform.localScale.y/Bubble.localScale.y);
        // 倒酒的速率
        float pourRate = BearColumn.localScale.x;
        bear.transform.localScale = new Vector3(bear.transform.localScale.x, 
            bear.transform.localScale.y + pourRate * BearSpeed *Time.deltaTime, 
            bear.transform.localScale.z);
        Bubble.localScale = new Vector3(Bubble.localScale.x,
            Bubble.localScale.y + pourRate * BearSpeed * BubbleSpeed *Time.deltaTime,
            Bubble.localScale.z);
        if (pourRate <= 0f)
        {
            float BubbleHeight = Bubble.localScale.y * Mathf.Exp(-BubbleDissapearSpeed * Time.deltaTime);
            if (BubbleHeight > bear.transform.localScale.y + offset)
            {
                Bubble.localScale = new Vector3(Bubble.localScale.x,
                    BubbleHeight,
                    Bubble.localScale.z);
            }
        }
    }
}
