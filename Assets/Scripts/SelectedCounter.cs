using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject gameObject;
    private void Start()
    {
        PlayerController.Instance.OnSelectedCounter += PlayerController_OnSelectedCounter;
    }

    private void PlayerController_OnSelectedCounter(object sender, PlayerController.OnSelectedCounterEventArgs e)
    {
        if(e.selectedCounter == clearCounter)
            gameObject.SetActive(true); //Show the visual game object of the counter
        else
            gameObject.SetActive(false); //Hide the visual game object of the counter
    }
}