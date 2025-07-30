using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;

    public override void Interact(PlayerController player)
    {
        //There is no kitchenObject on the clearCounter and the player carrying one that it is a cutKitchenObjectSO,
        //then place it on the clearCounter
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            if (GetOutputForInput(player.GetKitchenObject().GetScriptableKitchenObjects()) != null) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        //There is a kitchenObject on the clearCounter and the player is not carrying anything,then give it to the player
        else if (HasKitchenObject() && !player.HasKitchenObject())
            GetKitchenObject().SetKitchenObjectParent(player);
    }

    public override void InteractCutting(PlayerController player)
    {
        if (HasKitchenObject())
        {
            ScriptableKitchenObjects outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetScriptableKitchenObjects());
            if (outputKitchenObjectSO != null)
            {
                GetKitchenObject().DestroyKitchenObject();
            
                KitchenObject.SpwanKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private ScriptableKitchenObjects GetOutputForInput(ScriptableKitchenObjects inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSoArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return cuttingRecipeSO.output;
        }
        return null;
    }
}
