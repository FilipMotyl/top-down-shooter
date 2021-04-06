using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{

    public int bulletsLeft;
    protected override void Update()
    {
        if (isReloading == true)
        {
            return;
        }

        if (bulletsLeft <= 0 && ammoCurrent <= 0)
        {
            return;
        }
        if (ammoCurrent <= 0)
        {
            StartCoroutine(Reload());
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                //audiomanager play 
            }
                return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot(bulletSpawn.transform);
            
        }
    }

    public override void Shoot(Transform bulletSpawn)
    {
        
        base.Shoot(bulletSpawn);
        FindObjectOfType<AudioManager>().Play("Shoot Rifle");
    }
    protected override IEnumerator Reload()
    {
        Debug.Log("Reload");
        isReloading = true;
        FindObjectOfType<AudioManager>().Play("Rifle Reload");

        yield return new WaitForSeconds(reloadTime);

        if (bulletsLeft >= magazineCap)
        {
            ammoCurrent += magazineCap;
            bulletsLeft -= magazineCap;
        }
        else
        {
            ammoCurrent += bulletsLeft;
            bulletsLeft = 0;
        }
        isReloading = false;

    }
}
