using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(PlayerController player)
    {
        //Only accepts plates
        if (player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) { 
            DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
            
            player.GetKitchenObject().DestroyKitchenObject();
        }
    }
    
    
}
