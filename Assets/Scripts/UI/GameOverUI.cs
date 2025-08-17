using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    
    private void Start()
    {
        GameManager.Instance.OnStateChanged += (sender, args) =>
        {
            if (GameManager.Instance.IsGameOver()) {
                gameObject.SetActive(true);
                recipesDeliveredText.text = DeliveryManager.Instance.GetRecipesDeliveredNum().ToString();
            }
            else {
                gameObject.SetActive(false);
            }
        };
        gameObject.SetActive(false);
    }
    
}
