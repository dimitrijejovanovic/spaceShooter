using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public int hazardCount;
    private GameObject hazard;
    public GameObject player;


    public GUIText ammoText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText levelText;


    private bool gameOver;
    private bool restart;

    private bool endGame;

    private int score;
    private int level;
    private float pause;
    float speedInGame;


    void Start()
    {

        hazardCount = 15;
        speedInGame = -13;
        gameOver = false;
        restart = false;
        endGame = true;
        restartText.text = "";
        gameOverText.text = "";
        StartCoroutine(spawnWaves());
        score = 0;
        level = 1;
        updateAmmo();
        updateLevel();
    }

    void Update()
    {

        if (hazards.Length > 0)
        {
            foreach (GameObject h in hazards)
            {

                h.GetComponent<Mover>().speed = speedInGame - level / 2;

            }
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main_scene");
            }
        }
    }

    IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(1.5f);
        pause = 0.25f;


        while (endGame)
        {

            for (int i = 0; i < hazardCount; i++)
            {
                float xSpawn = Random.Range(-6, 6);
                Vector3 spawnPosition = new Vector3(xSpawn, 0.0f, 15.0f);
                Quaternion spawnRotation = Quaternion.identity;
                hazard = hazards[Random.Range(0, hazards.Length)];
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(pause);
            }

            hazardCount += 2;

            if (pause < 0.01f)
                pause = pause - (float)level / 2f;


            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }



            if (level < 20)
            {
                level++;
                updateLevel();
                yield return new WaitForSeconds(2f);
            }
            else endGame = false;

        }

        if (level >= 20 && !gameOver)
        {
            gameOverText.text = "Jeejoolah! " + (level * score) + " pts";
            gameOver = true;
            restartText.text = "Press 'R' for Restart";
            restart = true;
        }
    }


    public void updateAmmo()
    {
        ammoText.text = "Ammo: " + player.GetComponent<PlayerController>().ammo;
    }

    public void updateLevel()
    {
        levelText.text = "Level: " + level + "/20";
    }

    public void addScore(int x)
    {
        score += x;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over! " + (level * score) + " pts";
        gameOver = true;
    }


}
