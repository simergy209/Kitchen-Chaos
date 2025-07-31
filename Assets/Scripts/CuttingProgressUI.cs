using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CuttingProgressUI : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.OnCuttingProgress += (object sender, CuttingCounter.OnCuttingProgressEventArgs args) =>
        {
            progressBarImage.fillAmount = args.progressNormalized;
            if (args.progressNormalized == 0f || args.progressNormalized == 1f) 
                Hide();
            else
                Show();
        };
        progressBarImage.fillAmount = 0;
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
