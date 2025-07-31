using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if(player.HasKitchenObject())
            player.GetKitchenObject().DestroyKitchenObject();
    }
}
