using UnityEngine;

public class Bear : MonoBehaviour
{
    public Transform BearColumn;
    public Transform Bubble;
    public float BearSpeed;
    public float BubbleSpeed;
    public float BubbleDissapearSpeed;
    public float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float pourRate = BearColumn.localScale.x;
        transform.localScale = new Vector3(transform.localScale.x, 
            transform.localScale.y + pourRate * BearSpeed *Time.deltaTime, 
            transform.localScale.z);
        Bubble.localScale = new Vector3(Bubble.localScale.x,
            Bubble.localScale.y + pourRate * BearSpeed * BubbleSpeed *Time.deltaTime,
            Bubble.localScale.z);
        if (pourRate <= 0f)
        {
            float BubbleHeight = Bubble.localScale.y * Mathf.Exp(-BubbleDissapearSpeed * Time.deltaTime);
            if (BubbleHeight > transform.localScale.y*0.1 + offset)
            {
                Bubble.localScale = new Vector3(Bubble.localScale.x,
                    BubbleHeight,
                    Bubble.localScale.z);
            }
        }
    }
}
