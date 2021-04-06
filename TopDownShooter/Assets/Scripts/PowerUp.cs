using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int rifleAmmo;

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            PickUp(trigger.gameObject.GetComponent<Player>());
        }
    }
    
    private void PickUp(Player player)
    {
        player.healthPoints += health;
        player.weaponHolder.transform.GetChild(1).GetComponent<Rifle>().bulletsLeft += rifleAmmo;
        Debug.Log("Trigger");
        FindObjectOfType<AudioManager>().Play("Ammo Pickup");
        Destroy(this.gameObject);
    }
}
