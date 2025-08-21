using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private int previousCountdownNum;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += (sender, args) =>
        {
            if (GameManager.Instance.IsCountdownToStart()) {
                gameObject.SetActive(true);
            }
            else {
                gameObject.SetActive(false);
            }
        };
        gameObject.SetActive(false);
    }

    private void Update()
    {
        int countdownNum = GameManager.Instance.GetCountdownToStartTimer();
        countdownText.text = countdownNum.ToString();

        if (countdownNum == previousCountdownNum)
        {
            previousCountdownNum = countdownNum;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownToStartSound();
        }
    }
}
