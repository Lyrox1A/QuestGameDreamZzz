using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float moveSpeed = 3f;
    
    [Min(0)]
    [SerializeField] private float speedChangeRate = 5f;
    
    
    
    private Vector2 moveInput;
    
    private GameInput input;
    private InputAction moveAction;
    
    private CharacterController characterController;
    
    
    private void Awake()
    {
        input = new GameInput();
        moveAction = input.Player.Move;

        characterController = GetComponent<CharacterController>();

        //TODO Subscribe to input events
    }
    
    private void OnEnable()
    {
        input.Enable();
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        move(moveInput);
    }
    

    private void OnDisable()
    {
        input.Disable();
    }
    
    private void OnDestroy()
    {
        //TODO Unsubscribe to input events
    }

    private void move(Vector2 moveInput)
    {
        //Rotation
       
        if (moveInput != Vector2.zero) //rotate only if have new input 
        {
            Vector3 inputDirection = new Vector3(moveInput.x, y: 0f, z: moveInput.y).normalized; // Convert 2d movement to 3
            transform.rotation = Quaternion.LookRotation(forward: inputDirection, upwards: Vector3.up);
        }
        
        //Movement
        
        float targetSpeed = moveInput == Vector2.zero ? 0f : moveSpeed * moveInput.magnitude; // target speed = move speed if we have input otherwise its zero

        Vector3 currentVelocity = characterController.velocity;
        currentVelocity.y = 0f;
        float currentSpeed = currentVelocity.magnitude;

        if (Mathf.Abs( currentSpeed - targetSpeed) > 0.01f)
        {
            currentSpeed = Mathf.Lerp(a: currentSpeed, b: targetSpeed, t: Time.fixedDeltaTime * speedChangeRate);
        }
        else
        {
            currentSpeed = targetSpeed; 
        }





        Vector3 movement = transform.forward * currentSpeed;    

        characterController.SimpleMove(movement); 

    }
    
}
