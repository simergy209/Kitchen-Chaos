using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] gameObjectArray;
    private void Start()
    {
        PlayerController.Instance.OnSelectedCounter += PlayerController_OnSelectedCounter;
    }

    private void PlayerController_OnSelectedCounter(object sender, PlayerController.OnSelectedCounterEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            foreach (GameObject visualGameObject in gameObjectArray)
            {
                visualGameObject.SetActive(true); //Show the visual game object of the counter
            }
        }
        else
        {
            foreach (GameObject visualGameObject in gameObjectArray)
            {
                visualGameObject.SetActive(false); //Hide the visual game object of the counter
            }
        }
    }
}