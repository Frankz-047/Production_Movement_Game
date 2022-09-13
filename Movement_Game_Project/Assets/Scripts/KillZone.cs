using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}
