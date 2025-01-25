using UnityEngine;
using UnityEngine.SceneManagement;
public class EndCanvasController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    void Start()
    {
        GlobalEvent.OnGameEnd += OnGameEnd;
        canvasGroup.alpha = 0;
    }

    private void OnDestroy()
    {
        GlobalEvent.OnGameEnd -= OnGameEnd;
    }

    private void OnGameEnd()
    {
        canvasGroup.alpha = 1;
        TimeManager.TimeStop();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoadedEnd;

    }
    
    [SerializeField]
    private string menuSceneName = "Menu";
    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    private void OnSceneLoadedEnd(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoadedEnd;
        GameManager.Instance.Init();
    }
}
