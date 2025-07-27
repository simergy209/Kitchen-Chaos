using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private ScriptableKitchenObjects scriptableKitchenObjects;
    
    private ClearCounter clearCounter;

    public ScriptableKitchenObjects GetScriptableKitchenObjects()
    {
        //For reference of the kitchen object that was spawned 
        return scriptableKitchenObjects;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    //Change the kitchen object parent (the clear counter)
    {
        //this.clearCounter is the previous clear counter and clearCounter at the input is the new one
        //So we check if we have already parent we clear it and if not we set it to the clearCounter parameter
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }
        
        this.clearCounter = clearCounter;
        if (clearCounter.HasKitchenObject())
            Debug.LogError("Counter already has a kitchenObject");
        
        clearCounter.SetKitchenObject(this);
        
        transform.parent = clearCounter.GetKitchenObjectTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

}
