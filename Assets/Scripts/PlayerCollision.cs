using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    AudioManager am;

    private void Awake()
    {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        GameManager.Instance.onPlay.AddListener(ActivePlayer);  //Activate the Player when Playing
    }

    private void ActivePlayer()
    {
        gameObject.SetActive(true); //Activate the Player
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            gameObject.SetActive(false);    //Deactivate the Player when hitting an obstacle
            GameManager.Instance.GameOver();    //Set the mode to GameOver

            am.PlaySFX(am.gameOverSFX); //Play Soundeffect
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.AddScore(1);   //Add a point when hitting a coin
            Destroy(collision.gameObject);  //Destroy the coin

            am.PlaySFX(am.coinSFX); //Play Soundeffect
        }
    }
}
