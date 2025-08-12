using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeSOList  recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spwanRecipeTimer;
    private float spwanRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;


    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    //Spwan new recipe every 4 sec, unless we have already 4 recipes in the queue 
    {
        spwanRecipeTimer -= Time.deltaTime;
        if (spwanRecipeTimer <= 0) {
            spwanRecipeTimer = spwanRecipeTimerMax;
            
            if ( waitingRecipeSOList.Count < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) 
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            
            //Has the same number of ingredients
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) 
            {
                bool allIngredientsMatch = true;
                    
                 //Cycling over all the ingredients in the recipe 
                foreach (ScriptableKitchenObjects recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    //Cycling over all the ingredients in the plate
                    foreach (ScriptableKitchenObjects plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (recipeKitchenObjectSO == plateKitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    //After we over through all the ingredients in the plate and there is not a match 
                    if (!ingredientFound) {
                        allIngredientsMatch = false;
                    }
                }
                //After we found a match for all the ingredients in the plate and in the recipe 
                if (allIngredientsMatch) {
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        Debug.Log("Player did not delivered a correct recipe");
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}
