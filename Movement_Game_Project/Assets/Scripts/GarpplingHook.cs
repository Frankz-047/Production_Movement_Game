using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _grapplingHook;
    [SerializeField] private Transform _handPos;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private LayerMask _grappleLayer;
    [SerializeField] private float _maxGrappleDistance;
    [SerializeField] private float _hookSpeed;
    [SerializeField] private Vector3 _offset;

    private bool isShooting, isGrappling;
    private Vector3 _hookPoint;

    private void Start()
    {
        isShooting = false;
        isGrappling = false;
    }

    private void update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootHook();
        }

        if(isGrappling)
        {
            _grapplingHook.position = Vector3.Lerp(_grapplingHook.position, _hookPoint, _hookSpeed * Time.deltaTime);
            if (Vector3.Distance(_grapplingHook.position, _hookPoint) < 0.5f)
            {
                _controller.enabled = false;
                _playerBody.position = Vector3.Lerp(_playerBody.position, _hookPoint - _offset, _hookSpeed * Time.deltaTime);
                if (Vector3.Distance(_playerBody.position, _hookPoint - _offset) < 0.5f)
                {
                    _controller.enabled = true;
                    isGrappling = false;
                    _grapplingHook.SetParent(_handPos);
                }
            }

        }
    }

    private void ShootHook()
    {
        if (isShooting || isGrappling)
        {
            return;
        }

        isShooting = true;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, _maxGrappleDistance, _grappleLayer))
        {
            _hookPoint = hit.point;
            isGrappling = true;
            _grapplingHook.parent = null;
            _grapplingHook.LookAt(_hookPoint);
            
        }

        isShooting = false;
        
    }
}
