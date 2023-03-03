using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public Game game;
    public GameObject Text;

    public void Start()
    {
        Text.GetComponent<Text>().text = "Level: " + game.LevelIndex;
    }

    

    public void ButtonRestartPressed()
    {
        game.isButtonRestartCliked = true;
    }
}
