using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawner1;
    public Transform spawner2;
    public Transform spawner3;
    public Transform spawner4;

    public GameObject enemySmallPrefab;
    public GameObject enemyLargePrefab;
    public GameObject enemyPrefab;

    float time;
    public float spawnCooldown;

    public List<BoxCollider2D> ListOfEnemies;
    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            spawnEnemy();
        }
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = spawnCooldown;
            ListOfEnemies.RemoveAll(item => item == null);
            if (ListOfEnemies.Count < 12)
            {
                spawnEnemy();
            }
        }
  
    }
    void spawnEnemy()
    {
        RandomizeEnemy();

            int x = Random.Range(1, 5);
        if (x == 1 && spawner1)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawner1.position, spawner1.rotation);
            ListOfEnemies.Add(enemy.GetComponent<BoxCollider2D>()); 
        }
        else if (x == 2 && spawner2)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawner2.position, spawner2.rotation);
           
            ListOfEnemies.Add(enemy.GetComponent<BoxCollider2D>());
        }
        else if(x == 3 && spawner3)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawner3.position, spawner3.rotation);
         
            ListOfEnemies.Add(enemy.GetComponent<BoxCollider2D>());
        }
        else if(x == 4 && spawner4)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawner4.position, spawner4.rotation);

            ListOfEnemies.Add(enemy.GetComponent<BoxCollider2D>());
        }

    }
    void RandomizeEnemy()
    {
        int y = Random.Range(1, 3);
        if (y == 1)
        { enemyPrefab = enemySmallPrefab; }
        else if (y == 2)
        { enemyPrefab = enemyLargePrefab; }
    }
}
