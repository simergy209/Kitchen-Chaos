using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKitchenObjectSO(ScriptableKitchenObjects kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
