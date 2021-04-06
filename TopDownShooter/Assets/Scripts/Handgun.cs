using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : Weapon
{
    protected override void Update()
    {
        if (isReloading == true)
        {
            return;
        }
        if (ammoCurrent <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot(bulletSpawn.transform);

        }
    }

    public override void Shoot(Transform bulletSpawn)
    {
        base.Shoot(bulletSpawn);
        FindObjectOfType<AudioManager>().Play("Shoot Handgun");
    }
    protected override IEnumerator Reload()
    {
        Debug.Log("Reload");
        FindObjectOfType<AudioManager>().Play("Handgun Reload");
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammoCurrent += magazineCap;
        isReloading = false;
    }
}
