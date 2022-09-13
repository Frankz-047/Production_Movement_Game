using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 spawn;
    void Start()
    {
        spawn = this.transform.position;
    }
    public void Respawn()
    {
        this.transform.position = spawn;
    }
}
