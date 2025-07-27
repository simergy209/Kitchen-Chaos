using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private ScriptableKitchenObjects kitchenObjectSO;
    [SerializeField] private GameObject counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    public bool testing;
    
    private KitchenObject kitchenObject;

    private void Update()
    {
        //Just for testing
        if (testing && Input.GetKeyDown(KeyCode.T)) {
            if (kitchenObject != null) {
                kitchenObject.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }

    public void Interact(PlayerController player)
    {
        if (kitchenObject == null) {
            //Creates a copy of the kitchen object and places it above the counter 
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.GetPrefab().transform, counterTopPoint.transform);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else {
            //Give the object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    
    public Transform GetKitchenObjectTransform() {
        return counterTopPoint.transform;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
    
}
