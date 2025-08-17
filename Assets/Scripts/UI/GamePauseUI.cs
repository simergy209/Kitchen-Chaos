using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.PauseAndUnpauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += (sender, args) =>
        {
            gameObject.SetActive(true);
        };
        GameManager.Instance.OnGameUnpaused += (sender, args) =>
        {
            gameObject.SetActive(false);
        };
        
        gameObject.SetActive(false);
    }
}
