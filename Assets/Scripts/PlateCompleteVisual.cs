using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject 
    {
        public ScriptableKitchenObjects kitchenObjectSO;
        public GameObject gameObject;
    }
    
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List <KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Awake()
    //Disable all the kitchenObject that in plateCompleteVisual
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void Start()
    //When the event was called, We check which one of the kitchenObject that in plateCompleteVisual is the one that we have, and enable it
    {
        plateKitchenObject.OnIngredientAdded += (sender, args) => {
            foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList) {
                if(args.kitchenObjectSO == kitchenObjectSOGameObject.kitchenObjectSO)
                    kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        };
    }
}
