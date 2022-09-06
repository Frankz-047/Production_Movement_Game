using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    
    public float walkspeed;
    public float wallrunSpeed;
    public float groundDrag;
    
    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airForce;
    public float fallMult;
    public float maxJumpHeight = 5.0f;
    public float dashSpeed = 4.0f;
    public int jumpCharge = 2;
    bool CanJump;
    bool canDash;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;
    private float jumpPoint;

    [Header("Ground")]
    public float playerHeight;
    public LayerMask Ground;
    public bool isGround;
    bool checkGrounded;

    [Header("Setting")]
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        restricted,
        walking,
        wallrunning,
        air
    }

    //public bool sliding;
    //public bool crouching;
    public bool restricted; // no wasd movement
    public bool wallrunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        CanJump = true;
        canDash = true;
    }

    private void Update()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, Ground);

        if (!isGround && !checkGrounded)
        {
            checkGrounded = true;
        }
        if (isGround && checkGrounded)
        {
            ResetJump();
        }
        

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (isGround)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void FixedUpdate()
    {
        if (state != MovementState.restricted)
            MovePlayer();
        //if (rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * fallMult * Time.deltaTime;
        //}
        if (rb.position.y >= jumpPoint+maxJumpHeight)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMult * Time.deltaTime;
        }
        
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //To Jump
        if (Input.GetKeyDown(jumpKey) && CanJump)
        {
            --jumpCharge;
            if (!isGround)
            {
                --jumpCharge;
            }

            if (jumpCharge <= 0)
            {
                CanJump = false;
            }

            jumpPoint = rb.position.y;

            Jump();
        }
        //To Air Dash
        if (Input.GetKeyDown(dashKey) && !isGround && canDash)
        {
            AirDash();
        }
    }

    private void StateHandler()
    {
        // Mode - Restricted (no input)
        if (restricted)
        {
            state = MovementState.restricted;
        }

        // Mode - Wallrunning
        if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;
        }

        // Mode - Walking
        else if (isGround)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkspeed;
        }
        // Mode - Air
        else
        {
            state = MovementState.air;
        }
        // check if desired move speed has changed drastically
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();

            print("Lerp Started!");
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }


    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if (isGround)
            rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
        // in air
        else if (!isGround)
            rb.AddForce(moveDirection.normalized * walkspeed * 10f * airForce, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > walkspeed)
        {
            Vector3 limitedVel = flatVel.normalized * walkspeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        checkGrounded = false;
        CanJump = true;
        canDash = true;
        jumpCharge = 2;
    }
    private void AirDash()
    {
        Debug.Log(rb.velocity);
        Vector3 addVel = new Vector3(rb.velocity.x * dashSpeed, 1, rb.velocity.z * dashSpeed);
        rb.AddRelativeForce(addVel.normalized * walkspeed * dashSpeed, ForceMode.Force);
        Debug.Log(rb.velocity);
        canDash = false;
    }
}
