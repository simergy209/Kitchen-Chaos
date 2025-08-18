using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    //Static event because we have a few cuttingCounters 
    public static event EventHandler OnAnyCut;
    public static void ResetStaticData() {
        OnAnyCut = null; 
    }
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
    private int cuttingProgress;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

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
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / maxCuttingProgress });
            }
        }
        
        //There is a kitchenObject on the Counter and the player is not carrying anything,then give it to the player
        else if (HasKitchenObject() && !player.HasKitchenObject())
            GetKitchenObject().SetKitchenObjectParent(player);
        
        //There is a kitchenObject on the Counter and the player is carrying a plate, we add the kitchenObject to the plate
        else if (HasKitchenObject() && player.HasKitchenObject()) {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetScriptableKitchenObjects())) {
                    GetKitchenObject().DestroyKitchenObject();
                }
            }
        }
    }

    public override void InteractCutting(PlayerController player)
    {
        if (HasKitchenObject()) {
            ScriptableKitchenObjects outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetScriptableKitchenObjects());
            if (outputKitchenObjectSO != null) {
                cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                int maxCuttingProgress = getCuttingRecipeSO(GetKitchenObject().GetScriptableKitchenObjects()).maxCuttingProgress;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / maxCuttingProgress });
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
