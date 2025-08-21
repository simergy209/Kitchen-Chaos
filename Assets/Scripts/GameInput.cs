using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "inputBindings";
    public static GameInput Instance {get; private set;}
    
    private InputActionsPlayer inputActionsPlayer;
     
    
    public event EventHandler OnInteract;
    public event EventHandler OnInteractCutting;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;
    

    
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
        Gamepad_Interact,
        Gamepad_InteractAlternate,
        Gamepad_Pause,
    }
    private void Awake()
    { 
        Instance = this;
        inputActionsPlayer = new InputActionsPlayer(); //Construct the InputActionsPlayer
        
        //If the player changed the arrow key, we load the rebind input 
        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            inputActionsPlayer.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        
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
            case Binding.Gamepad_Interact:
                return inputActionsPlayer.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_InteractAlternate:
                return inputActionsPlayer.Player.InteractCutting.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause:
                return inputActionsPlayer.Player.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        inputActionsPlayer.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
                case Binding.MoveUp:
                    inputAction = inputActionsPlayer.Player.Move;
                    bindingIndex = 1;
                    break;
                case Binding.MoveDown:
                    inputAction = inputActionsPlayer.Player.Move;
                    bindingIndex = 2;
                    break;
                case Binding.MoveLeft:
                    inputAction = inputActionsPlayer.Player.Move;
                    bindingIndex = 3;
                    break;
                case Binding.MoveRight:
                    inputAction = inputActionsPlayer.Player.Move;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = inputActionsPlayer.Player.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.InteractAlternate:
                    inputAction = inputActionsPlayer.Player.InteractCutting;
                    bindingIndex = 0;
                    break;
                case Binding.Pause:
                    inputAction = inputActionsPlayer.Player.Pause;
                    bindingIndex = 0;
                    break;
                case Binding.Gamepad_Interact:
                    inputAction = inputActionsPlayer.Player.Interact;
                    bindingIndex = 1;
                    break;
                case Binding.Gamepad_InteractAlternate:
                    inputAction = inputActionsPlayer.Player.InteractCutting;
                    bindingIndex = 1;
                    break;
                case Binding.Gamepad_Pause:
                    inputAction = inputActionsPlayer.Player.Pause;
                    bindingIndex = 1;
                    break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback => {
            callback.Dispose();
            inputActionsPlayer.Player.Enable();
            onActionRebound();
            
            //Store the rebind input into a json file and in Awake we load it
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, inputActionsPlayer.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            
            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        })
        .Start();
    }
}
