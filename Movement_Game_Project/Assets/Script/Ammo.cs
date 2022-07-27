using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int destroyTimer;
    
    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

    public void OnTriggerEnter(Collider go)
    {
        PlayerHealth ph = go.gameObject.GetComponent<PlayerHealth>();
        RotaeTowars rt = go.gameObject.GetComponent<RotaeTowars>();
        if (ph != null)
        {
            ph.Hit();
            print("hit");
        }
        if (rt == null)
        {
            Destroy(gameObject);
        }
    }
}
