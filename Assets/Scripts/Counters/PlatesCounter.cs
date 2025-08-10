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
    //Checks if the time was passed (4 sec) and the number of plates on the counter is less the 4, we spwaned another one and call the event OnPlateSpwaned
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
    //Checks if there is a plate on the counter and the player holds nothing, we gave the plate to the player and call the event OnPlateSpwaned
    {
        if (spwanPlateAmount > 0 && !player.HasKitchenObject()) {
            spwanPlateAmount--;
            KitchenObject.SpwanKitchenObject(plateKitchenObjectSO, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
       
    }
}
