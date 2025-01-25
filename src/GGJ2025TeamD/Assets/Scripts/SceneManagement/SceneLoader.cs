using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [SceneName]
    public string SceneName;

    public void Load()
    {
        SceneManager.LoadScene(SceneName);
    }
}
