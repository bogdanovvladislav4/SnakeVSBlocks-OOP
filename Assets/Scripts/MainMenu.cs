using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Game game;
    private void Awake()
    {
        Time.timeScale = 0;
    }
    public void PlayPressed()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void PlayStartFirstLevelPressed()
    {
        SceneManager.LoadScene("Game");
        game.isButtoFirsttLevelCliked = true;
        Time.timeScale = 1;
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
