using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

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
        countdownText.text = GameManager.Instance.GetCountdownToStartTimer().ToString();
    }
}
