using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        //Only accepts plates
        if (player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) { 
            DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
            
            player.GetKitchenObject().DestroyKitchenObject();
        }
    }
    
    
}
