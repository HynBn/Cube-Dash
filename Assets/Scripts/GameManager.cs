using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;

        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public int currentScore = 0;
    public bool isPlaying = false;
    public bool isPaused = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public UnityEvent onPause = new UnityEvent();
    public UnityEvent onResume = new UnityEvent();

    AudioManager am;

    void Start()
    {
        am.PlayMenuMusic();
    }

    void Update()
    {
        if (isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        isPaused = false;
        currentScore = 0;
        Time.timeScale = 1;

        am.PauseMusic(am.menuMusic);
        am.PlayinGameMusic(true);
    }

    public void GameOver()
    {
        onGameOver.Invoke();
        currentScore = 0;
        isPlaying = false;
        Time.timeScale = 1;

        am.PlayMenuMusic();
        am.StopMusic(am.inGameMusic);
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
        isPaused = true;
        onPause.Invoke();
        Time.timeScale = 0;

        am.PlayMenuMusic();
        am.PauseMusic(am.inGameMusic);
    }

    public void ResumeGame()
    {
        isPaused = false;
        onResume.Invoke();
        Time.timeScale = 1;

        am.PauseMusic(am.menuMusic);
        am.PlayinGameMusic(false);
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
