using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    private enum State {
        waitingToStart,
        countingToStart,
        gamePlaying,
        gameOver,
    }

    private State state;

    private float waitingToStartTimer = 1f;
    private float countingToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;
    private bool isPaused = false;


    private void Awake()
    {
        Instance = this;
        state =  State.waitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += (sender, args) =>
        {
            PauseAndUnpauseGame();
        };
    }

    private void Update()
    {
        //State machine that updates the current state and call the OnStateChanged event 
        switch (state)
        {
            case State.waitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0) {
                    state = State.countingToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.countingToStart:
                countingToStartTimer -= Time.deltaTime;
                if (countingToStartTimer < 0) {
                    state = State.gamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0) {
                    state = State.gameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gameOver:
                break;
        }
    }
    
    public bool IsGamePlaying()
    {
        return state == State.gamePlaying;
    }

    public bool IsCountdownToStart()
    {
        return state == State.countingToStart;
    }

    public int GetCountdownToStartTimer()
    {
        return (int)countingToStartTimer + 1;
    }

    public bool IsGameOver()
    {
        return state == State.gameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void PauseAndUnpauseGame()
    {
        isPaused = !isPaused;
        if (isPaused) {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else {
            Time.timeScale = 1;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
