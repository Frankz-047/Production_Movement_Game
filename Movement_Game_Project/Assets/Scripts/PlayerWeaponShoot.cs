using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponShoot : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject ammo, spawnPoint;
    private bool canFire = true;
    public float fireingTimer, BulletsSpread, reloadTimer;
    public int ammocount, ammoSpeed, pushBack;
    private int maxAmmocount;
    public bool reloading = false;
    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        maxAmmocount = ammocount;
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ammocount == 0)
        {
            Reload();
        }
    }

    public void Reload()
    {
        if(!reloading)
        {
            StartCoroutine(ReloadTimer());
        }
    }

    public void Shoot()
    {
        if(canFire && ammocount > 0)
        {
            FireBulletMain();
            FireBulletSub();
            ammocount -= 1;
            StartCoroutine(FireTimer());
        }
    }

    private void FireBulletMain()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Quaternion rotation = mainCamera.transform.rotation;
        GameObject ammoObj = Instantiate(ammo, spawnPoint.transform.position, rotation);
        ammoObj.GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * ammoSpeed, ForceMode.Force);
        if (pm.onGround() == false)
        {
            player.GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * (pushBack * -1), ForceMode.Force);
        }
    }

    private void FireBulletSub()
    {
        for (int i = 0; i < 6; i++)
        {
            Quaternion spread = Quaternion.Euler(mainCamera.transform.rotation.eulerAngles + new Vector3(Random.Range(-BulletsSpread, BulletsSpread),
                Random.Range(-BulletsSpread, BulletsSpread), 0f));

            GameObject bullet = Instantiate(ammo, transform.position, spread);

            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ammoSpeed);
        }
    }

    private IEnumerator FireTimer()
    {
        canFire = false;
        yield return new WaitForSeconds(fireingTimer);
        canFire = true;
    }

    private IEnumerator ReloadTimer()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTimer);
        ammocount = maxAmmocount;
        reloading = false;
    }
}
