using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundController : MonoBehaviour
{
    [SerializeField]
    private float minValue = 0.5f;
    [SerializeField]
    private float maxValue = 1f;
    [SerializeField]
    private float minDistance = 0.2f;
    [SerializeField]
    private float maxDistance = 0.2f;
    
    [SerializeField]
    private float passMinAmount = 0f;
    [SerializeField]
    private float passMaxAmonut = 1f;
    [SerializeField]
    private int bonusCount = 0;

    [SerializeField]
    private GameObject cupPrefab;
    [SerializeField]
    private Transform cupContainer;
    private List<CupController> cupList = new List<CupController>();
    private CupController? nowCup;
    private CupController? preCup;
    
    public CupController? NowCup => nowCup;
    [SerializeField]
    private Transform cupLeftTransform;
    [SerializeField]
    private Transform cupRightTransform;
    [SerializeField]
    private Transform cupCenterTransform;

    private bool isOverHeight = false;

    private void Awake()
    {
        GlobalEvent.OnRoundEnd += OnRoundEnd;
        GlobalEvent.OnRoundReset += RoundReset;
        GlobalEvent.OnBubbleOverHeight += OnOverHeight;
        GlobalEvent.OnBearOverHeight += OnOverHeight;
    }

    private void Start()
    {
        bonusCount = 0;
    }

    private void OnDestroy()
    {
        GlobalEvent.OnRoundEnd -= OnRoundEnd;
        GlobalEvent.OnRoundReset -= RoundReset;
        GlobalEvent.OnBubbleOverHeight -= OnOverHeight;
        GlobalEvent.OnBearOverHeight -= OnOverHeight;
    }

    private void RoundReset()
    {
        CupController cup = this.GetCup();
        nowCup = cup;
        cup.transform.position = cupLeftTransform.position;
        moveToCenter = true;
        float distance = Random.Range(minDistance, maxDistance);
        passMinAmount = Random.Range(minValue, maxValue - distance);
        passMaxAmonut = passMinAmount + distance;
        isOverHeight = false;
        cup.gameObject.SetActive(true);
        cup.SetMaxAndMin(passMaxAmonut, passMinAmount);
        cup.Init();
    }

    private bool moveToCenter = false;
    private bool moveToRight = false;
    [SerializeField]
    private float cupMoveSpeed = 10f;

    private void OnOverHeight()
    {
        isOverHeight = true;
    }

    private void Update()
    {
        if (nowCup != null && moveToCenter)
        {
            nowCup.transform.position = Vector3.Lerp(nowCup.transform.position, cupCenterTransform.position, Time.deltaTime * cupMoveSpeed);
            if(Vector3.Distance(nowCup.transform.position, cupCenterTransform.position) < 0.1f)
            {
                moveToCenter = false;
                nowCup.transform.position = cupCenterTransform.position;
                GlobalEvent.RaiseRoundStart();
            }
        }

        if (preCup != null && moveToRight)
        {
            preCup.transform.position = Vector3.Lerp(preCup.transform.position, cupRightTransform.position, Time.deltaTime * cupMoveSpeed);
            if (Vector3.Distance(preCup.transform.position, cupRightTransform.position) < 0.1f)
            {
                moveToRight = false;
                preCup.transform.position = cupRightTransform.position;
                preCup.gameObject.SetActive(false);
            }
        }
    }

    private void OnRoundEnd()
    {
        preCup = nowCup;
        moveToRight = true;
        if (!isOverHeight && nowCup.AmountRate >= passMinAmount && nowCup.AmountRate <= passMaxAmonut)
        {
            
            GameManager.Instance.nowScore += 100 + Mathf.Max(bonusCount - 1, 0) * 10;
            bonusCount += 1; 
            
            GlobalEvent.RaiseScoreChange(GameManager.Instance.nowScore);
            GlobalEvent.RaiseRoundSuccess();
        }
        else
        {
            bonusCount = 0;
            GlobalEvent.RaiseRoundFail();
        }

        GlobalEvent.RaiseComboChange(bonusCount);

        GlobalEvent.RaiseRoundReset();
    }

    private CupController GetCup()
    {
        CupController cup = cupList.Find(c => !c.gameObject.activeSelf);
        if (cup == null)
        {
            cup = Instantiate(cupPrefab, cupContainer).GetComponent<CupController>();
            cupList.Add(cup);
        }
        return cup;
    }
}
