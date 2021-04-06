using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletPrefab;


    [SerializeField] protected int damage;
    [SerializeField] protected float bulletSpeed;

    [SerializeField] protected float fireRate;
    protected float nextTimeToFire = 0f;

    public float reloadTime;
    public int ammoCurrent;
    public int magazineCap;
    public bool isReloading = false;

    protected void Start()
    {
        ammoCurrent = magazineCap;
    }
    protected virtual void Update()
    {
        
    }
    private void OnEnable()
    {
        isReloading = false;
    }

    private void OnDisable()
    {
        FindObjectOfType<AudioManager>().StopReload();
    }


    public virtual void Shoot(Transform bulletSpawn)
    {
        //use ammo
        //instantiate bullet prefab
        //reload if no ammo (think how to fix if player change weapon on reload) - it should reset and start again when he activates weapon again
        ammoCurrent--;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSpawn.right * bulletSpeed, ForceMode2D.Impulse);
        
    }

    protected virtual IEnumerator Reload()
    {
        Debug.Log("Reload");
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammoCurrent += magazineCap;
        isReloading = false;
    }

}
