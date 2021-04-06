using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed;
    public int healthPointsMax;
    public int healthPoints;
    public Animator animator;
    public Rigidbody2D rb;

    public Transform weaponHolder;
    public Transform bulletSpawn;
    public Weapon currentWeapon;
    Vector2 movement;

    [SerializeField] private Slider healthPointBar;
    [SerializeField] private Slider ammoBar;
    [SerializeField] private AudioSource walking;
    private float timer;
    

    private void Awake()
    {
        healthPoints = healthPointsMax;
        weaponHolder.GetChild(0).gameObject.SetActive(true);
        weaponHolder.GetChild(1).gameObject.SetActive(false);
        currentWeapon = weaponHolder.GetChild(0).GetComponent<Weapon>();
    }
    private void Update()
    {
        //Read input for movement
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        //Rotate player
        PlayerRotate();
        //Animate player movement
        if (movement.x == 0 && movement.y == 0)
            animator.SetBool("isMoving", false);
        else
        {
            animator.SetBool("isMoving", true);
            if (GetComponent<AudioSource>().isPlaying == false)
            {

                walking.volume = Random.Range(0.1f, 0.2f);
                walking.pitch = Random.Range(0.8f, 1.1f);
                walking.Play(); 
            }

        }
        //listen for shooting
        

        if (Input.GetKeyDown("1"))
        {
            animator.SetBool("rifle", false);
            weaponHolder.GetChild(0).gameObject.SetActive(true);
            weaponHolder.GetChild(1).gameObject.SetActive(false);
            currentWeapon = weaponHolder.GetChild(0).GetComponent<Weapon>();
            timer = 0;
        }
        else if (Input.GetKeyDown("2"))
        {  
            animator.SetBool("rifle", true);
            weaponHolder.GetChild(0).gameObject.SetActive(false);
            weaponHolder.GetChild(1).gameObject.SetActive(true);
            currentWeapon = weaponHolder.GetChild(1).GetComponent<Weapon>();
            timer = 0;
        }
        UpdateUI();
    }

    private void FixedUpdate()
    {
        //Move player
        PlayerMove();
    }

    private void PlayerRotate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    private void PlayerMove()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateUI()
    {
        healthPointBar.value = (float)healthPoints / (float)healthPointsMax;

        if (currentWeapon.isReloading == true)
        {
            timer += Time.deltaTime;
            ammoBar.value = timer / (float)currentWeapon.reloadTime;
            if (timer >= currentWeapon.reloadTime)
            {
                timer = 0;
            }
        }
        else
        {
            ammoBar.value = (float)currentWeapon.ammoCurrent / (float)currentWeapon.magazineCap;
        }
    }
}


