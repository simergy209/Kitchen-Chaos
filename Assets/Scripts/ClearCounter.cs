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
        
        //There is a kitchenObject on the clearCounter and the player is not carrying anything,then give it to the player
        else if (HasKitchenObject() && !player.HasKitchenObject())
            GetKitchenObject().SetKitchenObjectParent(player);
    }
}
