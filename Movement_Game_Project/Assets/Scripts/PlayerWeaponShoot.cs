using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponShoot : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject ammo, spawnPoint;
    private bool canFire = true;
    public float fireingTimer, BulletsSpread;
    public int ammocount, reloadTimer, ammoSpeed, pushBack;
    private int maxAmmocount;
    public bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        maxAmmocount = ammocount;
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
        if (ammocount == 0 && !reloading)
        {
            StartCoroutine(ReloadTimer());
        }
    }
    public void Reload()
    {
        if (!reloading)
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
    public void Shoot()
    {
        if (canFire && ammocount > 0)
        {
            FireBulletMain();
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
        if (false)
        {
            player.GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * (pushBack * -1), ForceMode.Force);
        }
        FireBulletSub();
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
