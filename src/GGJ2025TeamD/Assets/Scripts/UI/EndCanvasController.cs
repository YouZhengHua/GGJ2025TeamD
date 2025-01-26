using UnityEngine;
using UnityEngine.SceneManagement;
public class EndCanvasController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    private BGMManager manager;
    
    [SerializeField]
    private GameObject perfactCup;
    [SerializeField]
    private GameObject failCup;
    [SerializeField]
    private GameObject overHeightCup;
    [SerializeField]
    private Transform cupTransform;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    void Start()
    {
        manager = FindObjectOfType<BGMManager>();
        GlobalEvent.OnGameEnd += OnGameEnd;
        canvasGroup.alpha = 0;
    }

    private void OnDestroy()
    {
        GlobalEvent.OnGameEnd -= OnGameEnd;
        TimeManager.TimeResume();
    }        


    private void OnGameEnd()
    {
        manager.PlaySound(BGMType.OVER);
        for (int i = 0; i < GameManager.Instance.SuccessCount; i++)
        {
            Instantiate(perfactCup, cupTransform);
        }

        for (int i = 0; i < GameManager.Instance.OverHeightCount; i++)
        {
            Instantiate(overHeightCup, cupTransform);
        }
        
        for (int i = 0; i < GameManager.Instance.FailCount; i++)
        {
            Instantiate(failCup, cupTransform);
        }
        
        canvasGroup.alpha = 1;
        TimeManager.TimeStop();
    }

    public void Restart()
    {
        manager.PlaySound(BGMType.GAMEON);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoadedEnd;

    }
    
    [SerializeField]
    private string menuSceneName = "Menu";
    public void BackToMenu()
    {
        manager.PlaySound(BGMType.START);
        SceneManager.LoadScene(menuSceneName);
    }

    private void OnSceneLoadedEnd(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoadedEnd;
        GameManager.Instance.Init();
    }
}
