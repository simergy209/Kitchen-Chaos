using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerAnimation : MonoBehaviour
{
    const string IS_WALKING = "IsWalking";
    [SerializeField] private PlayerController player; 
    private Animator animator;
    private void Update()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
