using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int destroyTimer;
    public bool playerWeaponAmmo;
    public string groundTag = "Ammo";
    
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
            if (go.gameObject.CompareTag("Player"))
            {
                print("player hit");
                ph.Hit();
            }
            if (rt == null)
            {
                Destroy(gameObject);
            }
        }
        if (playerWeaponAmmo)
        {
            if (rt != null)
            {
                rt.Hit();
                print("enemy hit");
            }
            if(go.gameObject.CompareTag(groundTag))
            {
                Destroy(gameObject);
            }
        }
    }
}
