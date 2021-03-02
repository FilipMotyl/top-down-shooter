using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour
{
    public Game_Manager gManager;
    public GameObject gameOver;



    private void RestartGame()
    {
        gameObject.SetActive(false);
        gManager.StartGame();
    }
}
