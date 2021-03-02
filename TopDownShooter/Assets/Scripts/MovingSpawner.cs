using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpawner : MonoBehaviour
{
    public int max;
    public int min;
    public bool direction;
    public float speed;
    public bool goingUp;
    public bool goingRight;

    private void Update()
    {
        //this is not a good code, but I was wondering if it will actually work this way.
        if (direction == true)
        {
            if (goingUp)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                if (transform.position.y > max)
                { goingUp = false; }
            }
            else if (!goingUp)
            {
                transform.Translate(Vector3.up * -speed * Time.deltaTime);
                if (transform.position.y < min)
                { goingUp = true; }
            }
        }
        else
        {
            if (goingRight)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (transform.position.x > max)
                { goingRight = false; }
            }
            else if (!goingRight)
            {
                transform.Translate(Vector3.right * -speed * Time.deltaTime);
                if (transform.position.x < min)
                { goingRight = true; }
            }
        }
    }
}
