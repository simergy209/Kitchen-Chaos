using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    private bool isWalking = false;
    [SerializeField] private GameInput gameInput;
    private Vector3 lastInteractDir;
    [SerializeField] private LayerMask counterLayerMask;
    
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
            if (canMove = !(Physics.CapsuleCast(transform.position, transform.position + Vector3.up * HeightPlayer, RadiusPlayer, inputVectorDirX, maxDistance)))
                transform.position += inputVectorDirX * moveSpeed * Time.deltaTime;
            else
            {
                Vector3 inputVectorDirZ = new Vector3(0, 0, inputVectorDir.z);
                if (canMove = !(Physics.CapsuleCast(transform.position, transform.position + Vector3.up * HeightPlayer, RadiusPlayer, inputVectorDirZ, maxDistance)))
                    transform.position += inputVectorDirZ * moveSpeed * Time.deltaTime;
            }
        }
        transform.forward = Vector3.Slerp(transform.forward, inputVectorDir, rotationSpeed * Time.deltaTime); //for player looks to the movement direction

        isWalking = (inputVectorDir != Vector3.zero); //isWalking=true if the player moving with arrows: W,A,S,D
    }

    private void HandleCollisions()
    {
        float maxDistance = 1.5f;
        Vector3 inputVectorDir = gameInput.GetInputPlayerDirection();
        
        //For cases where the player collides with counter and the button is not pressed anymore 
        if(inputVectorDir != Vector3.zero)
            lastInteractDir = inputVectorDir;
        
        //Checks if the player collide with the ClearCounter 
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, maxDistance, counterLayerMask))
        {
            ClearCounter clearCounter = hit.transform.GetComponent<ClearCounter>();
            if(clearCounter !=null)
                clearCounter.Interact();
        }
     
            
        
        
    }

}
