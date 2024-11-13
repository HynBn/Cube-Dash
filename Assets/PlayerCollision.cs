using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource coinSound;

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
            gameOverSound.Play(); //funktioniert nicht

            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(collision.gameObject);

            coinSound.Play();
        }
    }
}
