using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private ScriptableKitchenObjects scriptableKitchenObjects;
    
    private IKitchenObjectParent kitchenObjectParent;

    public ScriptableKitchenObjects GetScriptableKitchenObjects()
    {
        //For reference of the kitchen object that was spawned 
        return scriptableKitchenObjects;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    //Change the kitchen object parent (the clear counter)
    {
        //this.kitchenObjectParent is the previous kitchenObjectParent and kitchenObjectParent at the input is the new one
        //So we check if we have already parent we clear it and if not we set it to the kitchenObjectParent parameter
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
            Debug.LogError("IKitchenObjectParent already has a kitchenObject");
        
        kitchenObjectParent.SetKitchenObject(this);
        
        transform.parent = kitchenObjectParent.GetKitchenObjectTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroyKitchenObject()
    {
        GetKitchenObjectParent().ClearKitchenObject();
        Destroy(gameObject);
    }

    public static void SpwanKitchenObject(ScriptableKitchenObjects scriptableKitchenObjects, IKitchenObjectParent kitchenObjectParent)
    {
        //Creates a copy of the kitchen object and places it accordingly to the kitchenObjectParent
        Transform kitchenObjectTransform = Instantiate(scriptableKitchenObjects.GetPrefab().transform);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(kitchenObjectParent);
    }

}
