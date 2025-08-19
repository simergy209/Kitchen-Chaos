using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}
    
    private InputActionsPlayer inputActionsPlayer;
    public event EventHandler OnInteract;
    public event EventHandler OnInteractCutting;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
    }
    private void Awake()
    { 
        Instance = this;
        
        inputActionsPlayer = new InputActionsPlayer(); //Construct the InputActionsPlayer
        inputActionsPlayer.Player.Enable();
        
        //Adds the event(E keyboard) and checks if OnInteract!=null, calls the OnInteract(this, EventArgs.Empty)
        inputActionsPlayer.Player.Interact.performed += (InputAction.CallbackContext context) => { OnInteract?.Invoke(this, EventArgs.Empty); };
        
        //Adds the event(F keyboard) and checks if OnInteractCutting!=null, calls the OnInteractCutting(this, EventArgs.Empty)
        inputActionsPlayer.Player.InteractCutting.performed += (InputAction.CallbackContext context) => { OnInteractCutting?.Invoke(this, EventArgs.Empty); };

        inputActionsPlayer.Player.Pause.performed += (InputAction.CallbackContext context) => { OnPauseAction?.Invoke(this, EventArgs.Empty); };
    }
    
    private void OnDestroy()
    //This function called when the object is destroyed, and its unsubscribe from those events
    {
        inputActionsPlayer.Player.Interact.performed -= (InputAction.CallbackContext context) => { OnInteract?.Invoke(this, EventArgs.Empty); };
        inputActionsPlayer.Player.InteractCutting.performed -= (InputAction.CallbackContext context) => { OnInteractCutting?.Invoke(this, EventArgs.Empty); };
        inputActionsPlayer.Player.Pause.performed -= (InputAction.CallbackContext context) => { OnPauseAction?.Invoke(this, EventArgs.Empty); };
        
        //Clean up that object and free the memory
        inputActionsPlayer.Dispose();
    }

    public Vector3 GetInputPlayerDirection()
    {
        Vector2 inputVectorDir = inputActionsPlayer.Player.Move.ReadValue<Vector2>();
        inputVectorDir = inputVectorDir.normalized; //For walking diagonally (like right and forward together)
        return new Vector3(inputVectorDir.x, 0, inputVectorDir.y); 
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                return inputActionsPlayer.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return inputActionsPlayer.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return inputActionsPlayer.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return inputActionsPlayer.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return inputActionsPlayer.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return inputActionsPlayer.Player.InteractCutting.bindings[0].ToDisplayString();
            case Binding.Pause:
                return inputActionsPlayer.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        inputActionsPlayer.Player.Disable();

        inputActionsPlayer.Player.Move.PerformInteractiveRebinding(1).OnComplete(callback => {
            callback.Dispose();
            
            inputActionsPlayer.Player.Enable();
            onActionRebound();
        }).Start();
    }
}
