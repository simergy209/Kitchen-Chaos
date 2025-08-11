using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeSOList  recipeSOList;
    private List<RecipeSO> waitingRecipeSOList;
}
