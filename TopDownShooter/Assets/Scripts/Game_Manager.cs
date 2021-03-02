using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class Game_Manager : MonoBehaviour
{
    public Player player;
    public GameObject gameOver;
    public int finalScore;
    public Text scoreText;

    //Start Game Prefabs
    public GameObject playerPrefab;
    public GameObject spawnerPrefab;
    public GameObject mapPrefab;
    public GameObject startGame;

    //Cinemashine camera variables
    public CinemachineVirtualCamera vcam;
    public CinemachineConfiner vconf;

    public bool playAgain = false;
    public float timeToPlayAgain;



    [HideInInspector] public GameObject map;



    private void Update()
    {
        if (player != null && player.healthPoints <= 0)
        {
            GameOver();
        }
    }
    private void FixedUpdate()
    {
        if (gameOver.activeInHierarchy)
        {
            PlayAgain();
        }
    }
    private void GameOver()
    {

        timeToPlayAgain = Time.deltaTime;
        gameOver.SetActive(true);
        scoreText.text = "Score: " + finalScore;
        var Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var GameObject in Enemies)
        {
            Destroy(GameObject);
        }
        var Prefabs = GameObject.FindGameObjectsWithTag("Destroy");
        foreach (var GameObject in Prefabs)
        {
            Destroy(GameObject);
        }
        Destroy(player.gameObject);
        Destroy(map, 0.1f);
        Destroy(map.GetComponent<Spawner>().spawner1.gameObject);
        Destroy(map.GetComponent<Spawner>().spawner2.gameObject);
        Destroy(map.GetComponent<Spawner>().spawner3.gameObject);
        Destroy(map.GetComponent<Spawner>().spawner4.gameObject);

    }
    public void StartGame()
    {
        //Spawn 4 enemy spawners that will move along the walls.
        GameObject Spawner1 = Instantiate(spawnerPrefab, new Vector3(60.26f, -182.6f, 0), Quaternion.identity);
        Spawner1.GetComponent<MovingSpawner>().max = 180;
        Spawner1.GetComponent<MovingSpawner>().min = -60;
        Spawner1.GetComponent<MovingSpawner>().direction = false;

        GameObject Spawner2 = Instantiate(spawnerPrefab, new Vector3(-84.7f, -27.5f, 0), Quaternion.identity);
        Spawner2.GetComponent<MovingSpawner>().max = 60;
        Spawner2.GetComponent<MovingSpawner>().min = -180;
        Spawner2.GetComponent<MovingSpawner>().direction = true;

        GameObject Spawner3 = Instantiate(spawnerPrefab, new Vector3(58.8f, 78.1f, 0), Quaternion.identity);
        Spawner3.GetComponent<MovingSpawner>().max = 180;
        Spawner3.GetComponent<MovingSpawner>().min = -60;
        Spawner3.GetComponent<MovingSpawner>().direction = false;

        GameObject Spawner4 = Instantiate(spawnerPrefab, new Vector3(220f, -182.6f, 0), Quaternion.identity);
        Spawner4.GetComponent<MovingSpawner>().max = 60;
        Spawner4.GetComponent<MovingSpawner>().min = -180;
        Spawner4.GetComponent<MovingSpawner>().direction = true;

        //Instantiate a map area and assign the spawners
        GameObject Map = Instantiate(mapPrefab, new Vector3(99.26f, -149.81f,0), Quaternion.identity);
        Map.GetComponent<Spawner>().spawner1 = Spawner1.transform;
        Map.GetComponent<Spawner>().spawner2 = Spawner2.transform;
        Map.GetComponent<Spawner>().spawner3 = Spawner3.transform;
        Map.GetComponent<Spawner>().spawner4 = Spawner4.transform;
        map = Map;
        
        //Instantiate player character
        GameObject Player = Instantiate(playerPrefab, new Vector3(68.4f, -24f,0), Quaternion.identity);
        player = Player.GetComponent<Player>();
        
        //Pass a player character to camera, so it will follow it.
        vcam.Follow = Player.transform;
        vconf.m_BoundingShape2D = Map.GetComponent<PolygonCollider2D>();
        

        Map.SetActive(true);
        startGame.SetActive(false);
        gameOver.SetActive(false);
    }
    void PlayAgain()
    {
        if (timeToPlayAgain > 3f)
        {
            if (Input.anyKey)
            {
                StartGame();
            }
        }
        else 
        { 
            timeToPlayAgain += Time.deltaTime;
        }
    }
}
