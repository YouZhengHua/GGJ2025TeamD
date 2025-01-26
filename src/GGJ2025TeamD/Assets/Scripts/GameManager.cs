using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "GameManager";
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private float nowTime = 0f;
    private bool isInGame = false;
    private float totalGameTime = 120f;
    public int nowScore = 0;

    public float NowTime => Mathf.Max(totalGameTime - Time.timeSinceLevelLoad, 0f);

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
        else
        {
            Destroy(this.gameObject);
        }
        GlobalEvent.OnGameStart += GameStart;
        GlobalEvent.OnRoundSuccess += OnRoundSuccess;
        GlobalEvent.OnBearOverHeight += OnOverHeight;
        GlobalEvent.OnBubbleOverHeight += OnOverHeight;
        GlobalEvent.OnRoundFail += OnRoundFail;
    }
    
    private bool isOverHeight = false;

    private int successCount = 0;
    private int failCount = 0;
    private int overHeightCount = 0;
    
    public int SuccessCount => successCount;
    public int FailCount => failCount;
    public int OverHeightCount => overHeightCount;
    
    
    private void OnRoundSuccess()
    {
        successCount += 1;
        GlobalEvent.RaiseScoreChange(nowScore);
    }
    
    private void OnRoundFail()
    {
        if (isOverHeight)
        {
            overHeightCount += 1;
        }
        else
        {
            failCount += 1;
        }
        isOverHeight = false;
    }

    private void OnOverHeight()
    {
        isOverHeight = true;
    }

    public void Init()
    {
        nowTime = 0f;
        nowScore = 0;
        TimeManager.TimeResume();

        Invoke("DelayGameStart", 0.1f);
    }

    private void DelayGameStart()
    {
        GlobalEvent.RaiseGameStart();
    }

    private void Update()
    {
        if (isInGame)
        {
            nowTime += Time.deltaTime;
            if (nowTime >= totalGameTime)
            {
                isInGame = false;
                GameEnd();
            }
        }
    }

    private void OnDestroy()
    {
        GlobalEvent.OnGameStart -= GameStart;
        GlobalEvent.OnRoundSuccess -= OnRoundSuccess;
        GlobalEvent.OnBearOverHeight -= OnOverHeight;
        GlobalEvent.OnBubbleOverHeight -= OnOverHeight;
        GlobalEvent.OnRoundFail -= OnRoundFail;
    }

    private void GameStart()
    {
        isInGame = true;
        successCount = 0;
        failCount = 0;
        overHeightCount = 0;
        GlobalEvent.RaiseRoundReset();
    }

    private void GameEnd()
    {
        isInGame = false;
        GlobalEvent.RaiseGameEnd();
    }
}