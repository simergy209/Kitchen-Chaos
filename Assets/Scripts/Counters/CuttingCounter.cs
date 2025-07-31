using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
    private int cuttingProgress;
    public event EventHandler<OnCuttingProgressEventArgs> OnCuttingProgress;

    public class OnCuttingProgressEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    public override void Interact(PlayerController player)
    {
        //There is no kitchenObject on the Counter and the player carrying one that it is a cutKitchenObjectSO,
        //then place it on the Counter
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            if (GetOutputForInput(player.GetKitchenObject().GetScriptableKitchenObjects()) != null) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
                int maxCuttingProgress = getCuttingRecipeSO(GetKitchenObject().GetScriptableKitchenObjects()).maxCuttingProgress;
                OnCuttingProgress?.Invoke(this, new OnCuttingProgressEventArgs { progressNormalized = (float)cuttingProgress / maxCuttingProgress });
            }
        }
        //There is a kitchenObject on the Counter and the player is not carrying anything,then give it to the player
        else if (HasKitchenObject() && !player.HasKitchenObject())
            GetKitchenObject().SetKitchenObjectParent(player);
    }

    public override void InteractCutting(PlayerController player)
    {
        if (HasKitchenObject()) {
            ScriptableKitchenObjects outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetScriptableKitchenObjects());
            if (outputKitchenObjectSO != null) {
                cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);
                int maxCuttingProgress = getCuttingRecipeSO(GetKitchenObject().GetScriptableKitchenObjects()).maxCuttingProgress;
                OnCuttingProgress?.Invoke(this, new OnCuttingProgressEventArgs { progressNormalized = (float)cuttingProgress / maxCuttingProgress });
                if (cuttingProgress >= getCuttingRecipeSO(GetKitchenObject().GetScriptableKitchenObjects())
                        .maxCuttingProgress) {
                    GetKitchenObject().DestroyKitchenObject();
                    KitchenObject.SpwanKitchenObject(outputKitchenObjectSO, this);
                }
            }
        }
    }

    private ScriptableKitchenObjects GetOutputForInput(ScriptableKitchenObjects inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = getCuttingRecipeSO(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
            return cuttingRecipeSO.output;
        else
            return null;
    }

    private CuttingRecipeSO getCuttingRecipeSO(ScriptableKitchenObjects inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSoArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return cuttingRecipeSO;
        }
        return null;
    }
}
