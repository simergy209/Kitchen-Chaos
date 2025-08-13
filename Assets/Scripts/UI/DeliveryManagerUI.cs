using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipespwaned += (sender, args) =>
        {
            UpdateVisual();
        };

        DeliveryManager.Instance.OnRecipeCompleted += (sender, args) =>
        {
            UpdateVisual();
        };
        UpdateVisual();
    }
    
    //First we destroy all the recipes from the container (except the recipeTemplate) 
    //Then we spwan all the recipes that wait
    private void UpdateVisual()
    {
        foreach (Transform child in container) {
            if (child ==  recipeTemplate) 
                continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
