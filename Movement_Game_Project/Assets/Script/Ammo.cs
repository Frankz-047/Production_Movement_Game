using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int destroyTimer;
    public bool playerWeaponAmmo;
    
    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

    public void OnTriggerEnter(Collider go)
    {
        PlayerHealth ph = go.gameObject.GetComponent<PlayerHealth>();
        RotaeTowars rt = go.gameObject.GetComponent<RotaeTowars>();
        if (playerWeaponAmmo == false)
        {
            if (ph != null)
            {
                ph.Hit();
                print("player hit");
            }
            if (rt == null)
            {
                Destroy(gameObject);
            }
        }
        if (playerWeaponAmmo)
        {
            if (rt == null)
            {
                rt.Hit();
                print("enemy hit");
            }
            if (ph == null)
            {
                Destroy(gameObject);
            }
        }
    }
}
