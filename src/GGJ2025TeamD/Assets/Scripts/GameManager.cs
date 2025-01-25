using System.Runtime.CompilerServices;
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
                _instance = FindObjectOfType<GameManager>();
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

    public float NowTime => Mathf.Max(totalGameTime - nowTime, 0f);

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        nowTime = 0f;
        Time.timeScale = 1f;
        GlobalEvent.OnGameStart += GameStart;
        GlobalEvent.RaiseGameStart();
    }

    private void Update()
    {
        if (isInGame)
        {
            nowTime += Time.deltaTime;

            if (nowTime >= totalGameTime)
            {
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
        
        GlobalEvent.RaiseRoundStart();
    }

    private void GameEnd()
    {
        isInGame = false;
        GlobalEvent.RaiseGameEnd();
    }
}