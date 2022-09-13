using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public GameObject respawnPoint;

    private void Dead()
    {
        Destroy(gameObject);
    }

    public void Hit()
    {
        health--;
        if(health <= 0)
        {
            Dead();
        }

        gameObject.transform.position = respawnPoint.transform.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Ammo ammo = collision.gameObject.GetComponent<Ammo>();
        if (ammo != null)
        {
            Hit();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("KillZone") == true)
        {
            Hit();
        }
    }
}
