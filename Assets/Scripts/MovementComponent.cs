using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MovementComponent : MonoBehaviour
{
    //movement variables
    [SerializeField] float walkSpeed =5;
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpForce =5;

    //components
    private PlayerController playerController;
    private Rigidbody rigidbody;
    private Animator playeranimator;

    //movement references
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;

    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");

    
    public void Awake()
    {
        playeranimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerController.isJumping) return;
        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        //rigidbody.velocity = 
        transform.position += movementDirection;

    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        playeranimator.SetFloat(movementXHash, inputVector.x);
        playeranimator.SetFloat(movementYHash, inputVector.y);

        
    }
    
    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playeranimator.SetBool(isRunningHash, playerController.isRunning);

    }
    
    public void OnJump(InputValue value)
    {
        if (playerController.isJumping)
        {
            return;
        }
        playerController.isJumping = true;
        rigidbody.AddForce((transform.up + moveDirection)*jumpForce,ForceMode.Impulse);
        
        playeranimator.SetBool(isJumpingHash, playerController.isJumping);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        playeranimator.SetBool(isJumpingHash,false);
    }
}
