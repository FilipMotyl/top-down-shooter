using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is a generic bullet class, instantionated every time player shoots
/// </summary>
public class Bullet : MonoBehaviour
{

    public GameObject bloodSplatPrefab;
    private void Awake()
    {
        Destroy(this.gameObject, 5f);
    }

    /// <summary>
    /// Deal damage on monster collision, or disappear on wall collision.
    /// </summary>
    /// <param name="collision"></param> Object, the bullet collides with.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().hitPoints -= 5;
            Instantiate(bloodSplatPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        
    }
}
