using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private ScriptableKitchenObjects kitchenObjectSO;
    

    public override void Interact(PlayerController player)
    {
        //There is no kitchenObject on the clearCounter and the player carrying one, then place it on the clearCounter
        if(!HasKitchenObject() && player.HasKitchenObject())
            player.GetKitchenObject().SetKitchenObjectParent(this);
        
        //There is a kitchenObject on the clearCounter and the player is not carrying anything, then give it to the player
        else if (HasKitchenObject() && !player.HasKitchenObject()) 
            GetKitchenObject().SetKitchenObjectParent(player);
        
        else if (HasKitchenObject() && player.HasKitchenObject()) {
            //There is a kitchenObject on the clearCounter and the player is carrying a plate, we add the kitchenObject to the plate
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetScriptableKitchenObjects())) {
                    GetKitchenObject().DestroyKitchenObject();
                }
            }
            else {
                //There is a plate on the counter and the player is carrying something except plate, we add this to the plate
                if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                    if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetScriptableKitchenObjects()))
                        player.GetKitchenObject().DestroyKitchenObject();
                }
                   
            }
        }
    }
}
