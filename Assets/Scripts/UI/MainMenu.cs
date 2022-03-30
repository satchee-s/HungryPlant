using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string scene;

    public void StartGame()
    {
        SceneManager.LoadScene(scene);
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
