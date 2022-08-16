using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //for dandelion 
    public int i_numberOfBullets;

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
	void Update () {

        if(RotaeScript != null)
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
        else
        {
            if (canFire)
            {
                FireBullet();
                StartCoroutine(FireTimer());
            }
        }
       
    }

	private void FireBullet()
    {
  
        if (RotaeScript != null)
        {
            Quaternion rotation = spawnPoint.transform.rotation;
            GameObject ammoObj = Instantiate(ammo, spawnPoint.transform.position, rotation);
            
        }
        else
        {
            Quaternion rotation = this.transform.rotation;
            float Angledifference = 360 / i_numberOfBullets;
  
            for (int x = 0; x <= i_numberOfBullets; x ++ )
            {
                GameObject ammoObj = Instantiate(ammo, spawnPoint.transform.position, rotation);
                rotation.y += Angledifference;
                this.transform.Rotate(0,rotation.y,0,Space.World);

            }
            this.transform.rotation = new Quaternion(0,0,0,0);
        }

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
