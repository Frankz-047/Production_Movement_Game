using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using 

public class DespawnPlatform : MonoBehaviour
{
    public float lifetime = 5.0f;
    public float displacement = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = this.transform.position;
        position[1] -= displacement;
        this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
