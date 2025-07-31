using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimator : MonoBehaviour
//Creates the Open Close animations of container counter when the player press on E key arrow
{
    private const string OPEN_CLOSE = "OpenClose";
    private Animator animator;
    
    [SerializeField] private ContainerCounter containerCounter;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += (sender, args) =>
        {
            animator.SetTrigger(OPEN_CLOSE);
        };
    }
}
