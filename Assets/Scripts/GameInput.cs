using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputActionsPlayer inputActionPlayer;
    private void Awake()
    { 
        inputActionPlayer = new InputActionsPlayer(); //Consruct the InputActionsPlayer
        inputActionPlayer.Player.Enable(); 
    }

    public Vector3 GetInputPlayerDirection()
    {
        Vector2 inputVectorDir = inputActionPlayer.Player.Move.ReadValue<Vector2>();
        inputVectorDir = inputVectorDir.normalized; //For walking diagonally (like right and forward together)
        return new Vector3(inputVectorDir.x, 0, inputVectorDir.y); 
    }
}
