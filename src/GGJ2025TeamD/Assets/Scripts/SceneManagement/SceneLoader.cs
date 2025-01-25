using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [SceneName]
    public string SceneName;

    public void Load()
    {
        SceneManager.LoadScene(SceneName);

        SceneManager.sceneLoaded += OnSceneLoadedEnd;
    }

    private void OnSceneLoadedEnd(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoadedEnd;
        GameManager.Instance.Init();
    }
}
