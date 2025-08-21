using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private float burnShowProgressAmount = 0.5f;
        
    
    private void Start()
    {
        stoveCounter.OnProgressChanged += (sender, args) =>
        {
            if(stoveCounter.IsFried() && args.progressNormalized >= burnShowProgressAmount)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        };
        
        gameObject.SetActive(false);
    }
    
}
