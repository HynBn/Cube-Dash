using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject endMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private GameObject settingsMenuUI;

    GameManager gm;
    AudioManager am;

    private void Awake()
    {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverUI);

        gm.onPause.AddListener(ActivatePauseMenu);
        gm.onResume.AddListener(DeactivatePauseMenu);
    }

    void Update()
    {
        
    }

    public void PlayButtonHandler()
    {
        PlayButtonClickSound();
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
        scoreUI.text = gm.Score();
    }
}
