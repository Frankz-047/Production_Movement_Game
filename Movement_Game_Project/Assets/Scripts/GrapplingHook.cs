using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer lineRender;
    public GameObject player;
    public Transform grappligHook;
    public Transform handPos;
    public Transform playerBody;
    public LayerMask grappleLayer;
    public float maxGrappleDistance;
    public float hookSpeed;
    public Vector3 offset;

    private bool isGrappling;
    private Vector3 hookPoint;

        // Use this for initialization
    void Start()
    {
        isGrappling = false;
        lineRender.enabled = false;
    }

    void LateUpdate()
    {
        if (lineRender.enabled)
        {
            lineRender.SetPosition(0, handPos.position);
            lineRender.SetPosition(1, grappligHook.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ShootHook();
        }
        if (isGrappling)
        {
            // the player si grappling
            grappligHook.position = Vector3.Lerp(grappligHook.position, hookPoint, hookSpeed * Time.deltaTime); 
            if(Vector3.Distance(grappligHook.position, hookPoint) < 0.5f)
            {
                player.GetComponent<Rigidbody>().useGravity = false;
                if (Vector3.Distance(playerBody.position, hookPoint) < 4.0f)
                {
                    grappligHook.position = playerBody.position;
                    grappligHook.parent = handPos;
                    isGrappling = false;
                    player.GetComponent<Rigidbody>().useGravity = true;
                    lineRender.enabled = false;
                }
                else
                {
                    playerBody.position = Vector3.Lerp(playerBody.position, hookPoint, hookSpeed * Time.deltaTime);
                }
            }
        }
    }

    // shoot hook at the target
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
