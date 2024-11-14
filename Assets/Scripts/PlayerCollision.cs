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
        GameManager.Instance.onPlay.AddListener(ActivePlayer);
    }

    void Update()
    {
        
    }

    private void ActivePlayer()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();

            am.PlaySFX(am.gameOverSFX);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(collision.gameObject);

            am.PlaySFX(am.coinSFX);
        }
    }
}
