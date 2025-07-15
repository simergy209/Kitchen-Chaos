using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    private bool isWalking = false;
    
    private void Update()
    {
        Vector3 inputVectorDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
            inputVectorDir.z += 1;
        if(Input.GetKey(KeyCode.S))
            inputVectorDir.z -= 1;
        if(Input.GetKey(KeyCode.D))
            inputVectorDir.x += 1;
        if(Input.GetKey(KeyCode.A))
            inputVectorDir.x -= 1;
        
        inputVectorDir = inputVectorDir.normalized; //For walking diagonally (like right and forward together)
        transform.position += inputVectorDir * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, inputVectorDir, rotationSpeed * Time.deltaTime); //for player look to the movement direction

        isWalking = (inputVectorDir != Vector3.zero); //isWalking=true if the player moving with arrows: W,A,S,D
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}
