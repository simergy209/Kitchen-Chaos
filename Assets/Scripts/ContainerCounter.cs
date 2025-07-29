using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
//Spawn kitchen object accordingly the type of ContainerCounter when the player  press on E key arrow
{
    [SerializeField] private ScriptableKitchenObjects kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;  //Creates event for the open-close animation of the container counter
    
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject()) {
            
            KitchenObject.SpwanKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
