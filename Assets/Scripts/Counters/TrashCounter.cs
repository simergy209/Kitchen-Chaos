using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;
    
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject()) {
            OnAnyObjectTrashed(this, EventArgs.Empty);
            player.GetKitchenObject().DestroyKitchenObject();
        }
          
    }
}
