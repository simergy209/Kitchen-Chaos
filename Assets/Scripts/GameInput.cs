using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputActionsPlayer inputActionsPlayer;
    public event EventHandler OnInteract; 
    private void Awake()
    { 
        inputActionsPlayer = new InputActionsPlayer(); //Construct the InputActionsPlayer
        inputActionsPlayer.Player.Enable();
        
        //Adds the event and checks if OnInteract!=null, calls the OnInteract(this, EventArgs.Empty)
        inputActionsPlayer.Player.Interact.performed += (InputAction.CallbackContext context) =>
        {
            OnInteract?.Invoke(this, EventArgs.Empty);
        };
    }
    
    public Vector3 GetInputPlayerDirection()
    {
        Vector2 inputVectorDir = inputActionsPlayer.Player.Move.ReadValue<Vector2>();
        inputVectorDir = inputVectorDir.normalized; //For walking diagonally (like right and forward together)
        return new Vector3(inputVectorDir.x, 0, inputVectorDir.y); 
    }
}
