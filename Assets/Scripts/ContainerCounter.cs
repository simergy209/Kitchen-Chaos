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
        //Creates a copy of the kitchen object and places in front the player
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.GetPrefab().transform);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
