using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public ScriptableKitchenObjects kitchenObjectSO;
    }
    
    private List<ScriptableKitchenObjects> kitchenObjectSOList;
    [SerializeField] private List<ScriptableKitchenObjects> validKitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<ScriptableKitchenObjects>();
    }

    public bool TryAddIngredient(ScriptableKitchenObjects kitchenObjectSO)
    //Checks if the kitchenObject is a valid and there is not like it on the plate, we add it to the list and call the event OnIngredientAdded
    {
        if (validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
                return false;
            }
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
        else {
            return false;
        }
    }

    public List<ScriptableKitchenObjects> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }

  
    
}
