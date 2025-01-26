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
    }

    private void GameStart()
    {
        isInGame = true;
        
        GlobalEvent.RaiseRoundReset();
    }

    private void GameEnd()
    {
        isInGame = false;
        GlobalEvent.RaiseGameEnd();
    }
}