using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRender;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform grappligHook;
    [SerializeField] private Transform handPos;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform hookRope;
    [SerializeField] private LayerMask grappleLayer;
    public float maxGrappleDistance;
    public float hookSpeed;
    public Vector3 offset;

    private bool isGrappling;
    private bool isTeleporting;
    private bool isRetracting;
    private Vector3 hookPoint;

        // Use this for initialization
    void Start()
    {
        isGrappling = false;
        isTeleporting = false;
        isRetracting = false;
        lineRender.enabled = false;
    }

    void LateUpdate()
    {
        if (lineRender.enabled)
        {
            lineRender.SetPosition(0, player.transform.position);
            lineRender.SetPosition(1, grappligHook.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRetracting)
        {
            // if the hook is moving, retract the hook
            if (isGrappling)
            {
                isRetracting = true;
                isGrappling = false;
                isTeleporting = false;
            }
            // if the player is teleporting, stop the teleporting.
            if (isTeleporting)
            {
                grappligHook.position = handPos.position;
                grappligHook.parent = handPos;
                isTeleporting = false;
                player.GetComponent<Rigidbody>().useGravity = true;
                lineRender.enabled = false;
            }
        }
        if (isRetracting)
        {
            // the player is retracting the hook
            grappligHook.position = Vector3.Lerp(grappligHook.position, handPos.position, hookSpeed * Time.deltaTime);
            if (Vector3.Distance(grappligHook.position, handPos.position) < 0.5f)
            {
                grappligHook.position = handPos.position;
                grappligHook.parent = handPos;
                lineRender.enabled = false;
                isRetracting = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ShootHook();
        }
        if (isGrappling)
        {
            // the player is grappling
            grappligHook.position = Vector3.Lerp(grappligHook.position, hookPoint, hookSpeed * Time.deltaTime); 
            if(Vector3.Distance(grappligHook.position, hookPoint) < 0.5f)
            {
                player.GetComponent<Rigidbody>().useGravity = false;
                isGrappling = false;
                isTeleporting = true;
            }
        }

        if (isTeleporting)
        {
            if (Vector3.Distance(playerBody.position, hookPoint) < 4.0f)
            {
                grappligHook.position = handPos.position;
                grappligHook.parent = handPos;
                isTeleporting = false;
                player.GetComponent<Rigidbody>().useGravity = true;
                lineRender.enabled = false;
            }
            else
            {
                playerBody.position = Vector3.Lerp(playerBody.position, hookPoint, hookSpeed * Time.deltaTime);
            }
        }
    }

    // shoot the hook at the target
    private void ShootHook()
    {
        if (isGrappling) return;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, maxGrappleDistance, grappleLayer))
        {
            // shoot the hook, and then start to grapple
            isGrappling = true;
            grappligHook.parent = null;
            grappligHook.LookAt(hit.point);
            hookPoint = hit.point;
            lineRender.enabled = true;
        }
    }
}
