using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DespawnPlatform : MonoBehaviour
{
    public float lifetime = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);   
    }
    // Update is called once per frame
    void Update()
    {

    }
}
