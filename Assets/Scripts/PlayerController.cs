using System;
using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float moveSpeed = 3f; // Move Speed halt
    
    [Min(0)]
    [SerializeField] private float speedChangeRate = 5f; // Beschleunigung quasi

    [Min(0)]
    [SerializeField] private float rotationSpeed = 10f; // Rotationsgeschwindigkeit 

    [Header("Camera")]
    [Tooltip("The focus and rotation point of the camera")]
    [SerializeField] private Transform cameraTarget; //Transformfeld Objekt Selection

    [Range(-89f , 0f)]
    [SerializeField] private float VerticalCameraRotationMin = -30f; //Mind. Kamerarotation 
    
    [Range(0f , 89f)]
    [SerializeField] private float VerticalCameraRotationMax = 70f; //Max. Kamerarotation 
    
    [Min(0)]
    [Tooltip("Sense of Horizontal Cam rotation")]
    [SerializeField] private float cameraHorizontalSpeed = 200f; // Sense Schnelligkeit der Kamera in Grad
    
    [Min(0)]
    [Tooltip("Sense of Vertical Cam rotation")]
    [SerializeField] private float cameraVerticalSpeed = 130f; // sense Schnelligkeit der Kamera in Grad

    [Header("Mouse Settings")]

    //TODO 
    [Range(0f, 2f)]
    [SerializeField] private float mouseCameraSense = 1f; //Maussense Multiplier
    
    //TODO 
    [Header("Controller Settings")]
    [Range(0f, 2f)]
    [SerializeField] private float controllerCameraSense = 1f; //Controllersense Multiplier
                                                               
    //TODO 
    [Tooltip("Invert Y-Axis for controller")]
    [SerializeField] private bool invertY = true; // invertY 

    [Header("Animations")]
    [SerializeField] private Animator animator;
    

    private GameInput input;
    private InputAction moveAction;
    private InputAction lookAction;
    
    
    private CharacterController characterController;

    private Vector2 moveInput;
    private Vector2 lookInput;
    
    private Quaternion characterTargetRotation;
    private Vector2 cameraRotation;

    private Interactable selectedInteractable; 
    
    private void Awake() // called once at the beginning if game object is active 
    {
        input = new GameInput(); // Create new Gameinput
        moveAction = input.Player.Move; // Variable Usage of the actions 
        lookAction = input.Player.Look;

        input.Player.Interact.performed += Interact;
        
        characterController = GetComponent<CharacterController>();

        //TODO Subscribe to input events
    }
    
    private void OnEnable()
    {
        input.Enable();
    }

    private void Update()
    {
        ReadInput();
        move(moveInput);
        UpdateAnimator();
    }

    private void LateUpdate()
    {
        RotateCamera(lookInput);
    }

    private void FixedUpdate()
    {
        
    }
    

    private void OnDisable()
    {
        input.Disable();
    }
    
    private void OnDestroy()
    {
        // Unsubscribe to input events
        input.Player.Interact.performed -= Interact;
    }

    private void OnTriggerEnter(Collider other)
    {
        TrySelectInteractable(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TryDeselectInteractable(other);
    }
    private void ReadInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        lookInput = lookAction.ReadValue<Vector2>();
    }
    
    private void move(Vector2 moveInput)
    {
        //Rotation
       
        if (moveInput != Vector2.zero) //rotate only if have new input 
        {
            Vector3 inputDirection = new Vector3(moveInput.x, y: 0f, z: moveInput.y).normalized; // Convert 2d movement to 3d

            Vector3 worldInputDirection = cameraTarget.TransformDirection(inputDirection);
            worldInputDirection.y = 0;
            
            characterTargetRotation = Quaternion.LookRotation(forward: worldInputDirection, upwards: Vector3.up);
            
            //smooth target rotation

            transform.rotation = Quaternion.Slerp(transform.rotation, characterTargetRotation, Time.deltaTime * rotationSpeed);
        }
        
        //Movement
        
        float targetSpeed = moveInput == Vector2.zero ? 0f : moveSpeed * moveInput.magnitude; // target speed = move speed if we have input otherwise its zero

        Vector3 currentVelocity = characterController.velocity;
        currentVelocity.y = 0f;
        float currentSpeed = currentVelocity.magnitude;

        if (Mathf.Abs( currentSpeed - targetSpeed) > 0.01f)
        {
            currentSpeed = Mathf.Lerp(a: currentSpeed, b: targetSpeed, t: Time.deltaTime * speedChangeRate);
        }
        else
        {
            currentSpeed = targetSpeed; 
        }


        Vector3 targetDirection = characterTargetRotation * Vector3.forward; 

        Vector3 movement = targetDirection * currentSpeed;    

        characterController.SimpleMove(movement); 

    }

    private void RotateCamera(Vector2 lookInput)
    {

        if (lookInput != Vector2.zero)
        {
            bool isMouseLook = IsMouseLook();

            float deltaTimeMultiplier = isMouseLook ? 1.0f : Time.deltaTime;
            float sensitivity = isMouseLook ? mouseCameraSense : controllerCameraSense;

            lookInput *= deltaTimeMultiplier * sensitivity;

            cameraRotation.x += lookInput.y * cameraVerticalSpeed * (invertY && !isMouseLook ? -1 : 1); 
            cameraRotation.y += lookInput.x * cameraHorizontalSpeed; 

            cameraRotation.x = NormalizeAngle(cameraRotation.x);
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, VerticalCameraRotationMin, VerticalCameraRotationMax);
            cameraRotation.y = NormalizeAngle(cameraRotation.y);
        }
        
        cameraTarget.rotation = Quaternion.Euler(cameraRotation.x , cameraRotation.y , 0f);
    }

    private bool IsMouseLook()
    {
        return lookAction.activeControl.name == "delta";
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360;

        if (angle < 0)
        {
            angle += 360;
        }

        if (angle > 180)
        {
            angle -= 360;
        }

        return angle;
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = characterController.velocity;
        velocity.y = 0;
        float speed = velocity.magnitude;
        
        animator.SetFloat("MovementSpeed", speed);

        //TODO only set to false if we are following for a few frames OR
        //TODO Check with raycast 
        animator.SetBool("Grounded", characterController.isGrounded);
    }

    private void Interact(InputAction.CallbackContext _)
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact();
        }
    }
    private void TrySelectInteractable(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable == null)
        {
            return;
        }

        if (selectedInteractable != null)
        {
            selectedInteractable.Deselect();
        }

        selectedInteractable = interactable;
        selectedInteractable.Select();

    }

    private void TryDeselectInteractable(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable == null)
        {
            return;
        }

        if (interactable == selectedInteractable )
        {
            selectedInteractable.Deselect();
            selectedInteractable = null;
        }
    }
}
