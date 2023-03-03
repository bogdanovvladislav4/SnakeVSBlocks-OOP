using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Snake snake;
    public GameObject GameOverMenu;
    public GameObject WinMenu;

    private AudioSource _audio;
    internal bool isButtonRestartCliked;
    internal bool isButtonNextLevelCliked;
    internal bool isButtoFirsttLevelCliked;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    public enum State
    {
        Playing,
        Won,
        Loss,
    }
    public State CurrentState { get; private set; }

    public void OnPlayerDied()
    {
        if (CurrentState != State.Playing) return;
        GameOverMenu.SetActive(true);
        if (isButtonRestartCliked) { 
            CurrentState = State.Loss;
            snake.enabled = false;
            Debug.Log("Game Over!");
            ReloadLevel();
            GameOverMenu.SetActive(false);
        } else
        {
            Time.timeScale = 0;
        }
    }

    public void OnPlayerReachedFinish()
    {
        if (CurrentState != State.Playing) return;
        if (LevelIndex == 3) LevelIndex = 0;
        WinMenu.SetActive(true);
        if (isButtonNextLevelCliked)
        {
            CurrentState = State.Won;
            snake.enabled = false;
            LevelIndex++;
            Debug.Log("You won!");
            ReloadLevel();
            WinMenu.SetActive(false);
            Time.timeScale = 1;
        } else
        {
            Time.timeScale = 0;
        }
        
    }

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt(LevelIndexKey, 1);
        private set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }
    private const string LevelIndexKey = "LevelIndex";

    private void ReloadLevel()
    {      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        _audio.Play();
    }

    public void Update()
    {
        if (isButtonRestartCliked) ReloadLevel();
        if (isButtoFirsttLevelCliked) ResetLevels();
        if (isButtonNextLevelCliked) OnPlayerReachedFinish();
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }

    public void ResetLevels()
    {
        LevelIndex = 1;
    }
}
