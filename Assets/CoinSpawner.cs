using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] coinPrefabs; 
    [SerializeField] private Transform coinParent;     
    public float coinSpawnTime = 5f;
    public float coinSpeed = 3f;
    private float coinLifeTime = 10f;                   

    private float timeUntilCoinSpawn;

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearCoins);
        GameManager.Instance.onPlay.AddListener(ResetCoinSpawner);
    }

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            SpawnCoinLoop();
        }
    }

    private void SpawnCoinLoop()
    {
        timeUntilCoinSpawn += Time.deltaTime;

        if (timeUntilCoinSpawn >= coinSpawnTime)
        {
            SpawnCoin();
            timeUntilCoinSpawn = 0f;
        }
    }

    private void SpawnCoin()
    {
        GameObject coinToSpawn = coinPrefabs[Random.Range(0, coinPrefabs.Length)];
        GameObject spawnedCoin = Instantiate(coinToSpawn, transform.position, Quaternion.identity);
        spawnedCoin.transform.parent = coinParent;

        Rigidbody2D coinRB = spawnedCoin.GetComponent<Rigidbody2D>();
        coinRB.velocity = Vector2.left * coinSpeed;

        Destroy(spawnedCoin, coinLifeTime);
    }

    private void ClearCoins()
    {
        foreach (Transform child in coinParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void ResetCoinSpawner()
    {
        timeUntilCoinSpawn = 0f;
    }
}
