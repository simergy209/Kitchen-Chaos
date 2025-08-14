using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
//The base counter that contains all the shared features 
{
    public static event EventHandler OnAnyObjectDroped;
    [SerializeField] private GameObject counterTopPoint;
    private KitchenObject kitchenObject;
    
    public virtual void Interact(PlayerController player)
    {
         Debug.LogError("BaseCounter Interact");
    }
    
    public virtual void InteractCutting(PlayerController player)
    {
        Debug.LogError("BaseCounter InteractCutting");
    }
    
    public Transform GetKitchenObjectTransform() {
        return counterTopPoint.transform;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
            OnAnyObjectDroped?.Invoke(this, EventArgs.Empty);
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
