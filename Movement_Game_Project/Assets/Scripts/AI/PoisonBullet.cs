using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBullet : MonoBehaviour
{
    public float poisonDuration;
    public Material poisonMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (!collision.GetComponent<WallPoisonComponent>())
            {

                if (!collision.GetComponent<WallPoisonComponent>())
                {
                    collision.gameObject.AddComponent<WallPoisonComponent>();


                    collision.gameObject.GetComponent<WallPoisonComponent>().f_PoisonDuration = poisonDuration;
                    collision.gameObject.GetComponent<WallPoisonComponent>().m_poisonMaterial = poisonMaterial;

                    collision.gameObject.AddComponent<WallPoisonComponent>().setPoisous();
                }
            }



        }
        

    }
}
