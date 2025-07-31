using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimator : MonoBehaviour
//Creates the cutting with knife animations of cutting counter when the player press on f key arrow
{
    private const string CUT = "Cut";
    private Animator animator;
    
    [SerializeField] private CuttingCounter cuttingCounter;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += (sender, args) =>
        {
            animator.SetTrigger(CUT);
        };
    }
}
