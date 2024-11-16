using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Menu")]
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject endMenuUI;
    [SerializeField] private GameObject pauseMenuUI;

    [Header("Scores")]
    [SerializeField] private TextMeshProUGUI scoreUI;   
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI highscoreUI;

    GameManager gm;
    AudioManager am;

    private void Awake()
    {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        gm = GameManager.Instance;

        //Set the Menu depending on Event
        gm.onGameOver.AddListener(ActivateGameOverUI);  
        gm.onPause.AddListener(ActivatePauseMenu);
        gm.onResume.AddListener(DeactivatePauseMenu);
    }

    public void PlayButtonHandler()
    {
        PlayButtonClickSound(); //Play the Button sound clip
        gm.StartGame();
    }

    public void QuitButtonHandler()
    {
        PlayButtonClickSound();
        Application.Quit();
    }

    public void ResumeButtonHandler()
    {
        PlayButtonClickSound();
        gm.ResumeGame();
    }

    public void ButtonSoundHandler()
    { 
        PlayButtonClickSound();
    }

    private void PlayButtonClickSound()
    {
        am.PlaySFX(am.buttonPressSFX);
    }

    public void ActivateGameOverUI()
    {
        endMenuUI.SetActive(true);

        gameOverScoreUI.text = "Your Score: " + gm.currentScore;
        highscoreUI.text = "The Highscore: " + gm.highScore;
    }

    public void ActivatePauseMenu()
    {
        pauseMenuUI.SetActive(true);
    }

    public void DeactivatePauseMenu()
    {
        pauseMenuUI.SetActive(false);
    }

    private void OnGUI()
    {
        scoreUI.text = gm.Score();  //update the in game Score UI
    }
}
