using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    //Its TextMeshProUGUI and not TextMeshPro because the recipeText that we use is in the canvas
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    //First we destroy all the ingredients icons from the iconContainer (except the iconTemplate) 
    //Then we spwan all the icons that in the current recipe and give them the right sprite
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;
        
        foreach (Transform child in iconContainer) {
            if (child ==  iconTemplate) 
                continue;
            Destroy(child.gameObject);
        }

        foreach (ScriptableKitchenObjects kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
