using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    bool MainMenu = true;
    public GameObject mainMenu, inGameUI;

    // Update is called once per frame
    void Update()
    {
        if (MainMenu && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        inGameUI.SetActive(true);
        mainMenu.SetActive(false);
        MainMenu = false;
    }
    void EndGame()
    {
        inGameUI.SetActive(true);
        mainMenu.SetActive(false);
        MainMenu = true;
    }
}
