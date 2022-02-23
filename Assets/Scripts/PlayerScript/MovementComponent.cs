using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
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
    public GameObject followTransform;

    //movement references
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 lookInput = Vector2.zero;
    
    public float aimSensitivity = 1.0f;
    
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadHash = Animator.StringToHash("isReloading");
    public readonly int verticalAimHash = Animator.StringToHash("VerticalAim");


    
    public void Awake()
    {
        playeranimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance.cursorActive)
        {
            AppEvents.InvokeMousecursorEnable(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //x-axis ratation
        followTransform.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);
        //
        followTransform.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.left);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        if (angle>180 && angle<300)
        {
            angles.x = 300;
        }
        else if (angle<180 && angle>70)
        {
            angles.x = 70;
        }

        followTransform.transform.localEulerAngles = angles;
        
        //rotate the play along with aim
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        if (playerController.isJumping) return;
        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        //rigidbody.velocity = 
        transform.position += movementDirection;
        
        followTransform.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.left);

        var angles1 = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle1 = followTransform.transform.localEulerAngles.x;

        float min = -60;
        float max = 70.0f;
        float range = max - min;
        float offsetToZero = 0 - min;
        float aimAngle = followTransform.transform.localEulerAngles.x;
        aimAngle = (aimAngle > 180) ? aimAngle - 360 : aimAngle;
        float val = (aimAngle + offsetToZero) / (range);
        print(val);
        playeranimator.SetFloat(verticalAimHash, val);


        if (angle1 > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if (angle < 180 && angle > 70)
        {
            angles1.x = 70;
        }

    }

    /*private float groundedAngle = 45;
    private void FixedUpdate()
    {
        RaycastHit hit;

        /*Debug.DrawRay(
            new Vector3(transform.position.x, GetComponent<Collider>().bounds.extents.y, transform.position.z),
            -Vector3.up, Color.red, Time.deltaTime, true);#1#
        if (Physics.Raycast(new Vector3(transform.position.x,GetComponent<Collider>().bounds.extents.y,transform.position.z),
            -Vector3.up,out hit, 0.000000000001f,LayerMask.GetMask("Ground")))
        {
            if (Vector3.Angle(hit.normal,Vector3.up)<groundedAngle)
            {
                playerController.isJumping = false;
                playeranimator.SetBool(isJumpingHash, false);
            }
        }
    }*/

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

    public void OnAim(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    }
    
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        //if we aim up down adjust aniations to have a mask taht will let us properly animate aim
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground") && !playerController.isJumping || rigidbody.velocity.y > 0) return;

        if (IsGroundCollision(other.contacts))
        {
            playerController.isJumping = false;
            playeranimator.SetBool(isJumpingHash,false);
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground") && !playerController.isJumping || rigidbody.velocity.y > 0) return;

        if (IsGroundCollision(other.contacts))
        {
            playerController.isJumping = false;
            playeranimator.SetBool(isJumpingHash,false);
        }
    }
    bool IsGroundCollision(ContactPoint[] contacts)
    {
        for (int i = 0; i < contacts.Length; i++)
        {
            if (1-contacts[i].normal.y<0.1f)
            {
                return true;
            }
        }
        return false;
    }
    
    
}
