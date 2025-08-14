using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum State {
        waitingToStart,
        countingToStart,
        gamePlaying,
        gameOver,
    }

    private State state;

    private float waitingToStartTimer = 1f;
    private float countingToStartTimer = 3f;
    private float gamePlayingTimer = 10f;
    
    private void Update()
    {
        switch (state)
        {
            case State.waitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0)
                    state = State.countingToStart;
                break;
            case State.countingToStart:
                countingToStartTimer -= Time.deltaTime;
                if(countingToStartTimer < 0)
                    state = State.gamePlaying;
                break;
            case State.gamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer < 0)
                    state = State.gameOver;
                break;
            case State.gameOver:
                break;
        }
        Debug.Log(state);
    }
}
