using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

//  Script used for game paused checks, storing settings levels, and running the game timer
public class GameManager : MonoBehaviour
{
    //  Singleton instance for reference by other scripts
    public static GameManager instance;

    //  Is game paused? (Toggled by player input)
    public bool gamePaused = false;

    //  Settings levels
    public float audioLevel = 0.5f;
    public int qualityLevel = 0;

    //  Timer variables
    public float timeRemaining = 601;
    public string timeText;
    private bool timerIsRunning;
    public int finalScore;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartTime()
    {
        timerIsRunning = true;
    }

    private void Update()
    {
        if(timerIsRunning && !gamePaused)
        {
            //  Countdown the timer
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            //  Stop timer and switch to end screen if time runs out
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                finalScore = PlayerController.instance.score;
                EndScreenUIController.isWin = true;
                SceneManager.LoadSceneAsync(2);
            }

            //  String formatting for displaying time on HUD
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
