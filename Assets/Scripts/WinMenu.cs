using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public Game game;
    public void ButtonRestartPressed()
    {
        game.isButtonRestartCliked = true;
    }

    public void ButtonNextLevelPressed()
    {
        game.isButtonNextLevelCliked = true;
    }
    public void ButtonMainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
