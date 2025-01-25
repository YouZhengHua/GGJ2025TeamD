using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class SoptCanvasController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField] private InputActionReference triggerAction;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerAction.action.performed += OnTriggered;
        triggerAction.action.Enable();
    }

    private void OnDestroy()
    {
        triggerAction.action.performed -= OnTriggered;
    }

    private void OnTriggered(InputAction.CallbackContext context)
    {
        Toggle();
    }

    private void Toggle()
    {
        canvasGroup.alpha = canvasGroup.alpha == 0 ? 1 : 0;
        Time.timeScale = canvasGroup.alpha == 0 ? 1 : 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.Init();
    }
    
    [SerializeField]
    private string menuSceneName = "Menu";
    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}