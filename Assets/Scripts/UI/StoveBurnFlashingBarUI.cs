using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";
    [SerializeField] private StoveCounter stoveCounter;

    private float burnShowProgressAmount = 0.5f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += (sender, args) =>
        {
            bool show = stoveCounter.IsFried() && args.progressNormalized >= burnShowProgressAmount;
            animator.SetBool(IS_FLASHING, show);
        };
        animator.SetBool(IS_FLASHING, false);
    }

}
