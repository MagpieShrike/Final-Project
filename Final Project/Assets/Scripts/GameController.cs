using System.Collections;
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

    private int points;
    private bool gameOver;
    private bool restart;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        UpdateScore();

        gameOver = false;
        restart = false;

        restartText.text = "";
        gameOverText.text = "";
        creditText.text = "";

        StartCoroutine (SpawnWaves());
    }

    private void Update()
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

        if(points >= 100)
        {
            gameOverText.text = "You Win!";
            creditText.text = "Created by Halie Chalkley";
            gameOver = true;
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        creditText.text = "Created by Halie Chalkley";
        gameOver = true;
    }
}
