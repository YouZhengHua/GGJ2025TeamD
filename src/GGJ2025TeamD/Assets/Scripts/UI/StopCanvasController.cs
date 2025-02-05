﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class StopCanvasController : MonoBehaviour
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
        canvasGroup.alpha = 0;
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
        
        if (canvasGroup.alpha == 1)
        {
            TimeManager.TimeStop();
        }
        else
        {
            TimeManager.TimeResume();
        }
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
