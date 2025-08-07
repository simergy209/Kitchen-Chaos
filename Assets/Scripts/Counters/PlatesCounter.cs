using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private ScriptableKitchenObjects plateKitchenObjectSO; 
    private float spwanPlateTimer; 
    private float spwanPlateTimerMax = 4f;
    private int spwanPlateAmount;
    private int spwanPlateAmountMax = 4;
    public event EventHandler OnPlateSpwaned;
    public event EventHandler OnPlateRemoved;
    

    private void Update()
    {
        spwanPlateTimer += Time.deltaTime;
        if(spwanPlateTimer > spwanPlateTimerMax) {
                spwanPlateTimer = 0;
                if (spwanPlateAmount < spwanPlateAmountMax) {
                    spwanPlateAmount++;
                    OnPlateSpwaned?.Invoke(this, EventArgs.Empty);
                }
        }
    }

    public override void Interact(PlayerController player)
    {
        if (spwanPlateAmount > 0 && !player.HasKitchenObject()) {
            spwanPlateAmount--;
            KitchenObject.SpwanKitchenObject(plateKitchenObjectSO, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
       
    }
}
