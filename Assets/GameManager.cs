using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    public int currentScore = 0;
    public bool isPlaying = false;
    public bool isPaused = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public UnityEvent onPause = new UnityEvent();
    public UnityEvent onResume = new UnityEvent();

    [SerializeField] private AudioSource inGameMusic;
    [SerializeField] private AudioSource menuMusic;

    void Start()
    {
        menuMusic.Play();
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

        menuMusic.Pause();
        inGameMusic.time = 0;
        inGameMusic.Play();
    }

    public void GameOver()
    {
        onGameOver.Invoke();
        currentScore = 0;
        isPlaying = false;
        Time.timeScale = 1;

        menuMusic.Play();
        inGameMusic.Stop();
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

        menuMusic.Play();
        inGameMusic.Pause();
    }

    public void ResumeGame()
    {
        isPaused = false;
        onResume.Invoke();
        Time.timeScale = 1;

        menuMusic.Pause();
        inGameMusic.Play();
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
