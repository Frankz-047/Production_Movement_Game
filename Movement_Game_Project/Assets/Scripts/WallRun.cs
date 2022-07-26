using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Wall Running Setting")]
    public LayerMask isWall;
    public LayerMask isGround;
    public float wallRunForce, maxWallrunTime, wallrunTime;

    [Header("Input")]
    private float horizontalInput, verticalInput;

    [Header("Detection")]
    public float wallCheckDistance, minJunpHeight;
    private bool wallRight, wallLeft;
    private RaycastHit leftWallHit, rightWallHit;

    [Header("Reference")]
    public Transform orientation;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CheckWall();
    }

    //private void FixedUpdate()
    //{
    //}
    
    private void CheckWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, isWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, isWall);
    }
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJunpHeight, isGround);
    }
    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            StartWallRun();
        }
        else 
        {
            EndWallRun();
        }
    }
    private void StartWallRun()
    {

    }
    private void WallRunning()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        // forward force
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // push to wall force
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
    }
    private void EndWallRun()
    {

    }
}