using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
//The base counter that contains all the shared features 
{
    [SerializeField] private GameObject counterTopPoint;
    private KitchenObject kitchenObject;
    
    public virtual void Interact(PlayerController player)
    {
         Debug.LogError("BaseCounter Interact");
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
