using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airForce;
    bool CanJump;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground")]
    public float playerHeight;
    public LayerMask Ground;
<<<<<<< Updated upstream
    bool isGround;
=======
    public bool isGround;
    public bool checkGrounded;
    int jumpCharge;
>>>>>>> Stashed changes

    [Header("Setting")]
    public Transform orientation;
    public KeyCode spawnPlatform = KeyCode.F;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public PlayerCam rotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        CanJump = true;
        jumpCharge = 2;
    }

    private void Update()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, Ground);

        // better solutions welcome, set this way so both only check for one frame
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

        // handle drag
        if (isGround)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

<<<<<<< Updated upstream
        if (Input.GetKey(jumpKey) && CanJump && isGround)
=======
        //To Jump
        if (Input.GetKeyDown(jumpKey) && CanJump)
>>>>>>> Stashed changes
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

            Jump();
        }

        if (Input.GetKeyDown(spawnPlatform))
        {
            Vector3 position = gameObject.transform.position;
            position.y -= (playerHeight * 0.55f);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGround)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (isGround)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airForce, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
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
        CanJump = true;
    }
}
