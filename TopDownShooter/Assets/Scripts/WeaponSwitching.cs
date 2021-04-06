using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Player player;
    public Weapon[] weapons;

    private void Start()
    {
        SelectWeapon();
        weapons = new Weapon[transform.childCount];

        int i = 0;
        foreach (Transform weapon in transform)
        {
            weapons[i] = weapon.GetComponent<Weapon>();
        }
     }



    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {

            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { selectedWeapon = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2) { selectedWeapon = 1; }

        if (selectedWeapon != previousSelectedWeapon)
        {
            SelectWeapon();
        }


    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
