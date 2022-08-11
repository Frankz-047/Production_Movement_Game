using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Wall Running Setting")]
    private float wallRunTimer;
    
    public LayerMask isWall;
    public LayerMask isGround;
    public float wallRunForce = 200f;
    public float wallRunJumpUpForce = 7f;
    public float wallRunJumpSideForce = 7f;
    public float maxWallRunTime = 1f;
    public float wallClimbSpeed;

    [Header("Input")]
    private float horizontalInput, verticalInput;
    private bool upwardsRunning;
    private bool downwardsRunning;
    
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Limitations")]
    public bool doJumpOnEndOfTimer = false;
    public bool resetDoubleJumpsOnNewWall = true;
    public bool resetDoubleJumpsOnEveryWall = false;
    public int allowedWallJumps = 1;

    [Header("Detection")]
    private float exitWallTimer;

    public float wallCheckDistance = 0.7f;
    public float minJumpHeight = 2f;
    public float exitWallTime = 0.2f;

    [Header("Gravity")]
    public bool useGravity = false;
    public float yDrossleSpeed;

    [Header("Reference")]
    public Transform orientation;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Other")]
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;
    private bool exitingWall;
    private bool wallRemembered;
    private Transform lastWall;
    private int wallJumpsDone;

    private void Start()
    {
        if (isWall.value == 0)
            isWall = LayerMask.GetMask("Default");

        if (isGround.value == 0)
            isGround = LayerMask.GetMask("Default");

        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        CheckWall();
        StateMachine();

        // if grounded, next wall is a new one
        if (pm.isGround && lastWall != null)
            lastWall = null;
    }

    private void FixedUpdate()
    {
        if (pm.wallrunning && !exitingWall)
            WallRunning();
    }
    private void StateMachine()
    {
        // Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        // State 1 - Wallrunning
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall)
        {
            // start wallrun
            if (!pm.wallrunning) StartWallRun();

            // wallrun timer
            wallRunTimer -= Time.deltaTime;

            if (wallRunTimer < 0 && pm.wallrunning)
            {
                if (doJumpOnEndOfTimer)
                    WallJump();

                else
                {
                    exitingWall = true;
                    exitWallTimer = exitWallTime;
                }
            }

            // wall jump
            if (Input.GetKeyDown(jumpKey)) 
                WallJump();
        }

        // State 2 - Exiting
        else if (exitingWall)
        {
            pm.restricted = true;

            if (pm.wallrunning)
                EndWallRun();

            if (exitWallTimer > 0)
                exitWallTimer -= Time.deltaTime;

            if (exitWallTimer <= 0)
                exitingWall = false;
        }

        // State 3 - None
        else
        {
            if (pm.wallrunning)
                EndWallRun();
        }

        if (!exitingWall && pm.restricted)
            pm.restricted = false;
    }

    private void CheckWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, isWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, isWall);

        if ((wallLeft || wallRight) && NewWallHit())
        {
            wallJumpsDone = 0;
            wallRunTimer = maxWallRunTime;
        }
    }
    private void RememberLastWall()
    {
        if (wallLeft)
            lastWall = leftWallHit.transform;

        if (wallRight)
            lastWall = rightWallHit.transform;
    }
    private bool NewWallHit()
    {
        if (lastWall == null)
            return true;

        if (wallLeft && leftWallHit.transform != lastWall)
            return true;

        else if (wallRight && rightWallHit.transform != lastWall)
            return true;

        return false;
    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, isGround);
    }
    private void StartWallRun()
    {
        pm.wallrunning = true;
        wallRunTimer = maxWallRunTime;
        rb.useGravity = useGravity;
        wallRemembered = false;
    }
    private void WallRunning()
    {
        rb.useGravity = useGravity;
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        float velY = rb.velocity.y;
        
        if (!useGravity)
        {
            if (velY > 0)
                velY -= yDrossleSpeed;

            rb.velocity = new Vector3(rb.velocity.x, velY, rb.velocity.z);
        }

        // forward force
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // upwards/downwards force
        if (upwardsRunning)
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        if (downwardsRunning)
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);

        if (!exitingWall && !(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);

        // remember the last wall
        if (!wallRemembered)
        {
            RememberLastWall();
            wallRemembered = true;
        }
    }
    private void EndWallRun()
    {
        rb.useGravity = true;
        pm.wallrunning = false;
    }

    public void WallJump()
    {

        bool firstJump = true;

        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 forceToApply = new Vector3();

        if (wallLeft)
            forceToApply = transform.up * wallRunJumpUpForce + leftWallHit.normal * wallRunJumpSideForce;

        else if (wallRight)
            forceToApply = transform.up * wallRunJumpUpForce + rightWallHit.normal * wallRunJumpSideForce;

        firstJump = wallJumpsDone < allowedWallJumps;
        wallJumpsDone++;

        // if not first jump, remove y component of force
        if (!firstJump)
            forceToApply = new Vector3(forceToApply.x, 0f, forceToApply.z);

        // add force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        RememberLastWall();

        // stop wallRun
        EndWallRun();
    }
}