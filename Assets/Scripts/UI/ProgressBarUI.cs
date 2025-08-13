using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBarImage;
    [SerializeField] private GameObject hasProgressGameObject;
    private IHasProgress hasProgress;

    private void Start()
    {
        //event for handles on the progress bar, creates a gameObject for hasProgress because this is an interface and cant expose on unity editor
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        
        if(hasProgress == null)
            Debug.LogError("Game Object" + hasProgressGameObject + " has no IHasProgress component");
        
        hasProgress.OnProgressChanged += (object sender, IHasProgress.OnProgressChangedEventArgs args) =>
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
