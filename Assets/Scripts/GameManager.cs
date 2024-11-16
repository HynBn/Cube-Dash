using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();    //Get the Audiomanager
    }

    public int currentScore = 0;
    public int highScore = 0;
    public bool isPlaying = false;
    public bool isPaused = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public UnityEvent onPause = new UnityEvent();
    public UnityEvent onResume = new UnityEvent();

    AudioManager am;

    void Start()
    {
        am.PlayMenuMusic(); //Play the MenuMusic when starting the Game
    }

    void Update()
    {
        if (isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();  //Activate Pause Menu only while Playing and when the ESC button is pressed
        }
    }

    public void StartGame()
    {
        onPlay.Invoke();    //activate onPlay Event

        isPlaying = true;   
        isPaused = false;

        currentScore = 0;
        Time.timeScale = 1; //if paused, set to normal

        am.PauseMusic(am.menuMusic);    //Pause the menuMusic
        am.PlayinGameMusic(true);   //Play the gameMusic (from the start)

        Cursor.visible = false;
    }

    public void GameOver()
    {
        if(highScore < currentScore)    //if a new highscore is obtained
        {
            highScore = currentScore;
        }

        onGameOver.Invoke();

        isPlaying = false;

        currentScore = 0;
        Time.timeScale = 1;

        am.PlayMenuMusic(); //Resume the menuMusic
        am.StopMusic(am.inGameMusic);   //Stop the gameMusic

        Cursor.visible = true;
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        onPause.Invoke();

        isPaused = true;
        isPlaying = false;

        Time.timeScale = 0; //Pause the game

        am.PlayMenuMusic();
        am.PauseMusic(am.inGameMusic);
        
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        onResume.Invoke();

        isPaused = false;
        isPlaying = true;

        Time.timeScale = 1; //Continue the Game

        am.PauseMusic(am.menuMusic);
        am.PlayinGameMusic(false);  //don't revert to start

        Cursor.visible = false;
    }

    public void AddScore (int amount)
    {
        currentScore += amount;
    }

    public string Score()
    {
        return currentScore.ToString();
    }


}
