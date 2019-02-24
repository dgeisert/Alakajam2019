using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public bool MainMenu = true;
    public GameObject mainMenu, inGameUI;
    float startTime;
    public float score;
    public Text scoreText;

    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenu && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        if (!MainMenu)
        {
            scoreText.text = "Score: " + Mathf.Floor(score + (Time.time - startTime) * 1000).ToString();
        }
    }

    void StartGame()
    {
        score = 0;
        startTime = Time.time;
        EnemySpawner.instance.SpawnEnemy();
        EnemySpawner.instance.SpawnEnemy();
        EnemySpawner.instance.SpawnEnemy();
        EnemySpawner.instance.SpawnEnemy();
        EnemySpawner.instance.SpawnEnemy();
        inGameUI.SetActive(true);
        mainMenu.SetActive(false);
        MainMenu = false;
    }
    public void Reset()
    {
        inGameUI.SetActive(false);
        mainMenu.SetActive(true);
        MainMenu = true;
    }
}
