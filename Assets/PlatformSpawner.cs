using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private Transform platformParent;
    public float platformSpawnTime = 3f;
    public float platformSpeed = 4f;
    [Range(0f, 1f)] public float platformSpawnTimeFactor = 0.1f;
    [Range(0f, 1f)] public float platformSpeedFactor = 0.2f;

    private float _platformSpawnTime;
    private float _platformSpeed;

    private float timeUntilplatformSpawn;
    private float timeAlive;

    private float platformLifeTime = 15f;

    void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearObsitcales);
        GameManager.Instance.onPlay.AddListener(ResetFactors);
    }

    void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            timeAlive += Time.deltaTime;

            CalculateFactors();
            SpawnLoop();
        }
    }

    private void SpawnLoop()
    {
        timeUntilplatformSpawn += Time.deltaTime;

        if (timeUntilplatformSpawn >= _platformSpawnTime)
        {
            Spawn();
            timeUntilplatformSpawn = 0;
        }
    }

    private void ClearObsitcales()
    {
        foreach (Transform child in platformParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void Spawn()
    {
        GameObject platformToSpawn = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        GameObject spawnedplatform = Instantiate(platformToSpawn, transform.position, Quaternion.identity);

        spawnedplatform.transform.parent = platformParent;

        Rigidbody2D platformRB = spawnedplatform.GetComponent<Rigidbody2D>();
        platformRB.velocity = Vector2.left * _platformSpeed;

        Destroy(spawnedplatform, platformLifeTime);
    }

    private void CalculateFactors()
    {
        _platformSpawnTime = platformSpawnTime / Mathf.Pow(timeAlive, platformSpawnTimeFactor);
        _platformSpeed = platformSpeed * Mathf.Pow(timeAlive, platformSpeedFactor);
    }

    private void ResetFactors()
    {
        timeAlive = 1f;
        _platformSpawnTime = platformSpawnTime;
        _platformSpeed = platformSpeed;
    }
}
