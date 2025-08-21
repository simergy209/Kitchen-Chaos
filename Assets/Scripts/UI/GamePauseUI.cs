using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }
    
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.PauseAndUnpauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += (sender, args) =>
        {
            Show();
        };
        GameManager.Instance.OnGameUnpaused += (sender, args) =>
        {
           Hide();
        };
        
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        
        //For the first button(RESUME BUTTON) will be selected immediately, handles the gamepad controller input
        resumeButton.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
