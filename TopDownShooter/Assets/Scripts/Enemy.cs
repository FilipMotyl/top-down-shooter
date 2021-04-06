using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;
    private Transform player;
    public int range;

    public int hitPoints;
    public int damage;
    public int points;
    public Animator animator;
    public bool isDead;
    public BoxCollider2D col;
    private Rigidbody2D rb;
    public Renderer rr;
    bool noScore;
    public GameObject explosionPrefab;
    public GameObject ammoDropPrefab;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (hitPoints <= 0)
        {
            
            Death();
        }
        if (!isDead)
        {
            FollowPlayer();
            RotateEnemy();
        }
    }

    void Death()
    {
        if (isDead == false)
        {
            animator.SetTrigger("isDead");
            
        }
        isDead = true;
        Destroy(col);
        Destroy(rb);
        rr.sortingOrder = -1;
        if (noScore == false)
        {
            if (Random.Range(1, 10) <= 2)
            {
                Instantiate(ammoDropPrefab, this.transform.position, this.transform.rotation);
            }
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().finalScore += points;
            noScore = true;
            FindObjectOfType<AudioManager>().Play("Demon Dying");
        }
    }
    void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) > range)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
        }
        else
        {
            Explosion();
        }

    }
    void RotateEnemy()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }

    void Explosion()
    {
        noScore = true;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        player.gameObject.GetComponent<Player>().healthPoints -= damage;
        FindObjectOfType<AudioManager>().Play("Demon Explosion");
        Death();
    }
}
