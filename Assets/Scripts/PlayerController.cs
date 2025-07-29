using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKitchenObjectParent
{
    public event EventHandler<OnSelectedCounterEventArgs> OnSelectedCounter;
    public class OnSelectedCounterEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    private bool isWalking = false;
    [SerializeField] private GameInput gameInput;
    private Vector3 lastInteractDir;
    [SerializeField] private LayerMask counterLayerMask;
    private BaseCounter selectedCounter;
    [SerializeField] private GameObject kitchenObjectHoldPoint;
    private KitchenObject kitchenObject;

    public static PlayerController Instance { get; private set; } //singleton pattern of player, equal to: new PlayerController()


    private void Awake()
    {
        //Checks if the instance .
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("There is more than one Player Instance");
    }

    private void Start()
    {
        //One listenner
        gameInput.OnInteract += (object sender, EventArgs e) =>
        {
            if (selectedCounter != null)
                selectedCounter.Interact(this);
        };
        gameInput.OnInteractCutting += (object sender, EventArgs e) =>
        {
            if (selectedCounter != null)
                selectedCounter.InteractCutting(this);
        };
    }
    
    private void Update()
    {
        HandelPlayerMovement();
        HandleCollisions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandelPlayerMovement()
    {
        Vector3 inputVectorDir = gameInput.GetInputPlayerDirection();
        float HeightPlayer = 2f;
        float RadiusPlayer = 0.7f;
        float maxDistance = moveSpeed * Time.deltaTime; //how can I determine this value? 
        
        //Physics.CapsuleCast() Returns true if the capsule(our player) intersects with a collider
        bool canMove = !(Physics.CapsuleCast(transform.position, transform.position + Vector3.up * HeightPlayer, RadiusPlayer, inputVectorDir, maxDistance));
        
        if (canMove)
            transform.position += inputVectorDir * moveSpeed * Time.deltaTime; 
        
        //In case that 2 arrows key(like W+A) were pressed, the player could proceed in the direction that he can move on
        if (!canMove)
        {
            Vector3 inputVectorDirX = new Vector3(inputVectorDir.x, 0, 0);
            
            //Trying to move in the X axis and there is nothing on there, then we can move
            canMove = inputVectorDir.x != 0 && !(Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * HeightPlayer, RadiusPlayer, inputVectorDirX, maxDistance));
            if (canMove)
                transform.position += inputVectorDirX * moveSpeed * Time.deltaTime;
            else
            {
                Vector3 inputVectorDirZ = new Vector3(0, 0, inputVectorDir.z);
                
                //Trying to move in the Z axis and there is nothing on there, then we can move
                canMove = inputVectorDir.z != 0 && !(Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * HeightPlayer, RadiusPlayer, inputVectorDirZ, maxDistance));
                if (canMove)
                    transform.position += inputVectorDirZ * moveSpeed * Time.deltaTime;
            }
        }
        transform.forward = Vector3.Slerp(transform.forward, inputVectorDir, rotationSpeed * Time.deltaTime); //for player looks to the movement direction

        isWalking = (inputVectorDir != Vector3.zero); //isWalking=true if the player moving with arrows: W,A,S,D
    }

    private void HandleCollisions()
    {
        Vector3 inputVectorDir = gameInput.GetInputPlayerDirection();
        
        //For cases where the player collides with counter and the button is not pressed anymore 
        if(inputVectorDir != Vector3.zero)
            lastInteractDir = inputVectorDir;
        
        float maxDistance = 1.5f;
        //Checks if the player collide with the ClearCounter then set the selectedCounter to clearCounter
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, maxDistance, counterLayerMask))
        {
            BaseCounter baseCounter = hit.transform.GetComponent<BaseCounter>();
            if (baseCounter != null)
            {
                if (selectedCounter != baseCounter)
                    SetSelectedCounter(baseCounter);
            }
            else
                   SetSelectedCounter(null);
        }
        else 
            SetSelectedCounter(null);
            
        //Debug.Log(selectedCounter);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounter?.Invoke(this, new OnSelectedCounterEventArgs { selectedCounter = selectedCounter }); //If OnSelectedCounter!=null, calls the OnSelectedCounter with the parameters
    }
    public Transform GetKitchenObjectTransform() {
        return kitchenObjectHoldPoint.transform;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
