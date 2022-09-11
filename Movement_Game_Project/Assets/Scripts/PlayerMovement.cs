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
<<<<<<< Updated upstream
=======
    public float fallMult = 2f;
>>>>>>> Stashed changes
    bool CanJump;

    [Header("Ground")]
    public float playerHeight;
    public LayerMask Ground;
    public bool isGround;

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
    }

    private void Update()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, Ground);
<<<<<<< Updated upstream
=======

        if (!isGround && !checkGrounded)
        {
            checkGrounded = true;
        }
        if (isGround && checkGrounded)
        {
            ResetJump();
        }

>>>>>>> Stashed changes
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
        {
            MovePlayer();
        }
    }

<<<<<<< Updated upstream
    public void Walk(float horizontal, float vertical)
    {
        horizontalInput=horizontal;
        verticalInput =vertical;
=======
    public void Walk(float horIn, float verIn)
    {
        horizontalInput = horIn;
        verticalInput = verIn;
>>>>>>> Stashed changes
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

    public void Jump()
    {
<<<<<<< Updated upstream
        Debug.Log("Jump");
        if (CanJump && isGround)
        {
            Debug.Log("Jump Done");
            CanJump = false;

            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
=======
        //To Jump
        if (CanJump)
        {
            CanJump = false;
            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //jump
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
>>>>>>> Stashed changes
        }
    }
    private void ResetJump()
    {
        CanJump = true;
    }

    public bool onGround()
    {
        return isGround;
    }
    public bool GetCanJump() { return CanJump; }
}
