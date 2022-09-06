using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIType
{
    Tower,
    Dandelion
}
public class RangeAttack : MonoBehaviour {
  
	public float speedUp, speed, killTimer;
    public float fireDealy = 0.5f;
    private float trueSpeed,  trueKillTimer;
    private int fireTime = 0;
    public bool speedUpLode = false;
	private bool canFire = true; 
	public GameObject ammo,spawnPoint;
    private GameObject player;
	private RotaeTowars RotaeScript;
    public AIType AIcurType = AIType.Dandelion;


    // Use this for initialization
    void Start () {
        trueSpeed = speed;
        trueKillTimer = killTimer * (1.0f / fireTime);
        killTimer = trueKillTimer;
		RotaeScript = gameObject.GetComponent<RotaeTowars>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(outPut());
    }

    // Update is called once per frame
    void Update()
    {

        if (RotaeScript.ReadyToAttack() && canFire)
        {

            FireBullet();
            StartCoroutine(FireTimer());
        }
        if (RotaeScript.ReadyToAttack() && speedUpLode == false)
        {
            StopCoroutine(outOfSight());
            speedUpLode = true;
            StartCoroutine(inSight());
        }
        if (RotaeScript.ReadyToAttack() == false)
        {
            StartCoroutine(outOfSight());
        }
    }
    
     

	private void FireBullet()
    {

       Quaternion rotation = spawnPoint.transform.rotation;
       GameObject ammoObj = Instantiate(ammo, spawnPoint.transform.position, rotation);

    }

	private IEnumerator FireTimer()
    {
        canFire = false;
        yield return new WaitForSeconds (fireDealy);
        canFire = true;
    }

    private IEnumerator outOfSight()
    {
        yield return new WaitForSeconds(5);
        speed = trueSpeed;
        killTimer = trueKillTimer;
    }

    private IEnumerator inSight()
    {
        speed += speedUp;
        killTimer -= 1;
        if(killTimer <= 0)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            ph.Hit();
        }
        yield return new WaitForSeconds(0.2f);
        speedUpLode = false;
    }

    private IEnumerator outPut()
    {
        yield return new WaitForSeconds(1);
        print("the ammo speed ia at " + speed);
        StartCoroutine(outPut());
    }

 
}
