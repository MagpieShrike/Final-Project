﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text pointText;
    public Text gameOverText;
    public Text restartText;
    public Text creditText;

    public AudioClip bgMusic;
    public AudioClip bossMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public AudioSource musicSource;

    private int points;
    private bool gameOver;
    private bool restart;

    GameObject boss;

    

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindWithTag("Boss");

        if (SceneManager.GetActiveScene().name == "Space Shooter")
        {
            musicSource.clip = bgMusic;
            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Boss Battle")
        {
            musicSource.clip = bossMusic;
            musicSource.Play();
        }
        

        points = 0;
        UpdateScore();

        gameOver = false;
        restart = false;

        restartText.text = "";
        gameOverText.text = "";
        creditText.text = "";

        StartCoroutine (SpawnWaves());
    }

     void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }

        if (Input.GetKey("escape"))
            Application.Quit();

        bossState();
    }

    void bossState()
    {
        if (SceneManager.GetActiveScene().name == "Boss Battle" && musicSource.clip != winMusic)
        {
            if (!boss)
            {
                musicSource.clip = winMusic;
                musicSource.Play();

                gameOverText.text = "You Win!";
                creditText.text = "Created by Halie Chalkley";
                gameOver = true;
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'X' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newPointValue)
    {
        points += newPointValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        pointText.text = "Points: " + points;

        if(points >= 100 && SceneManager.GetActiveScene().name == "Space Shooter")
        {
            gameOverText.text = "You Win!";
            creditText.text = "Created by Halie Chalkley";
            gameOver = true;

            SceneManager.LoadScene("Boss Battle");      
        }

    }

    public void GameOver()
    {
        musicSource.clip = loseMusic;
        musicSource.Play();
        gameOverText.text = "Game Over";
        creditText.text = "Created by Halie Chalkley";
        gameOver = true;
    }


}
