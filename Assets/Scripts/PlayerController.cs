using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    private void Update()
    {
        Vector3 inputVector = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.W))
            inputVector.z += 1;
        if(Input.GetKey(KeyCode.S))
            inputVector.z -= 1;
        if(Input.GetKey(KeyCode.D))
            inputVector.x += 1;
        if(Input.GetKey(KeyCode.A))
            inputVector.x -= 1;
        
        inputVector = inputVector.normalized; //For walking diagonally (like right and forward together)
        transform.position += inputVector * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, inputVector, rotationSpeed * Time.deltaTime); //for player look to the movement direction

    }

}
