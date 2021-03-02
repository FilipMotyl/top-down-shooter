using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed;
    public bool isMoving;
    public int healthPoints;

    public Animator animator;
    public Rigidbody2D rb;

    Vector2 movement;

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
            animator.SetBool("isMoving", true);
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
}
