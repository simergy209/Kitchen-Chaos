using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image TimerImage;

    private void Start()
    {
        TimerImage.fillAmount = 0;
    }

    private void Update()
    {
        TimerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
