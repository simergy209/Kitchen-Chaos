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
        progressBarImage.fillAmount = 0;
        cuttingCounter.OnCuttingProgress += (object sender, CuttingCounter.OnCuttingProgressEventArgs args) =>
        {
            progressBarImage.fillAmount = args.progressNormalized;
        };
    }
}
