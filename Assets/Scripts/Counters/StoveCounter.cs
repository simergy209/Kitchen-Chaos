using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;

    public class OnStateChangeEventArgs : EventArgs {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;
    

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject()) {
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)fryingTimer / fryingRecipeSO.maxFryingProgress });
                    if (fryingTimer > fryingRecipeSO.maxFryingProgress) {
                        //The meat was fried
                        GetKitchenObject().DestroyKitchenObject();
                        KitchenObject.SpwanKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0;
                        burningRecipeSO = getBurningRecipeSO(GetKitchenObject().GetScriptableKitchenObjects());
                        
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs() { state = state });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)burningTimer / burningRecipeSO.maxBurningProgress });
                    if (burningTimer > burningRecipeSO.maxBurningProgress) {
                        //The meat was Burned
                        GetKitchenObject().DestroyKitchenObject();
                        KitchenObject.SpwanKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs() { state = state });
                         OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0 });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(PlayerController player)
    {
        //There is no kitchenObject on the Counter and the player carrying one that it is a cutKitchenObjectSO,
        //then place it on the Counter
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            if (GetOutputForInput(player.GetKitchenObject().GetScriptableKitchenObjects()) != null) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingRecipeSO = getFryingRecipeSO(GetKitchenObject().GetScriptableKitchenObjects());
                state = State.Frying;
                fryingTimer = 0;
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs() { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)fryingTimer / fryingRecipeSO.maxFryingProgress });
            }
        }
        //There is a kitchenObject on the Counter and the player is not carrying anything,then give it to the player
        else if (HasKitchenObject() && !player.HasKitchenObject()) {
            GetKitchenObject().SetKitchenObjectParent(player);
            state = State.Idle;
            OnStateChange?.Invoke(this, new OnStateChangeEventArgs() { state = state });
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0 });

        }
            
        
    }
    
    private ScriptableKitchenObjects GetOutputForInput(ScriptableKitchenObjects inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = getFryingRecipeSO(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
            return fryingRecipeSO.output;
        else
            return null;
    }

    private FryingRecipeSO getFryingRecipeSO(ScriptableKitchenObjects inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
                return fryingRecipeSO;
        }
        return null;
    }
    
    private BurningRecipeSO getBurningRecipeSO(ScriptableKitchenObjects inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
                return burningRecipeSO;
        }
        return null;
    }
}
