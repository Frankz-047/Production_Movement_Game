using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandellionAI : MonoBehaviour
{
    //for dandelion 
    public int i_numberOfBulletsX, i_numberOfBulletsY, bulletDestroyTime;
    public float fireDealy = 0.5f;
    public GameObject ammo, spawnPoint;
    internal bool canFire = true;

    //rottion
    private RotaeTowars rotator;
    // Start is called before the first frame update
    void Start()
    {
        rotator = this.gameObject.GetComponent<RotaeTowars>();
        rotator.RotationStatus(false);
        ammo.GetComponent<Ammo>().destroyTimer = bulletDestroyTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            FireBullets();
            StartCoroutine(FireTimer());
        }
    }


    private IEnumerator FireTimer()
    {
        canFire = false;
        yield return new WaitForSeconds(fireDealy);
        canFire = true;
    }
    void FireBullets()
    {
        Quaternion rotation = this.transform.rotation;
        float XAngledifference = 360 / i_numberOfBulletsX;
        float YAngleDifference = 90 / i_numberOfBulletsY;
        for (int y = 0; y <= i_numberOfBulletsY; y++)
        {
            for (int x = 0; x <= i_numberOfBulletsX; x++)
            {
                if (x >= (i_numberOfBulletsX / 2))
                {
                    YAngleDifference *= -1;
                    rotation.x = 0;
                }
                GameObject ammoObj = Instantiate(ammo, spawnPoint.transform.position, rotation);
                
                rotation.y += XAngledifference;
                rotation.x += YAngleDifference;
                this.transform.Rotate(rotation.x, rotation.y, 0, Space.World);

            }
        }
        this.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
