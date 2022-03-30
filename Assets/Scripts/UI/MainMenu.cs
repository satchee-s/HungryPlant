using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("_MainScene 1");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void Settings()
    {

    }

    public void Continue()
    {

    }
}
