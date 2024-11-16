using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnerController : MonoBehaviour
{
    [Header("Object Prefabs")]
    [SerializeField] private GameObject[] prefabs;

    [Header("Object Container")]
    [SerializeField] private Transform objectParent;

    [Header("Object Spawn and Speed values")]
    public float spawnTime;
    public float speed;

    //increased difficulty values
    private float _spawnTime;   
    private float _speed;

    [Header("Object Spawn and Speed increase Factor")]
    [Range(0f, 1f)] public float spawnTimeFactor;
    [Range(0f, 1f)] public float speedFactor;

    private float timeUntilSpawn;
    private float timeAlive;
    private float lifeTime = 10f;

    void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearObjects);  //when GameOver, destroy all objects
        GameManager.Instance.onPlay.AddListener(ResetFactors);  //when starting the Game, reset everything
    }

    void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            timeAlive += Time.deltaTime;

            CalculateFactors(); //increase difficulty
            SpawnLoop();    //spawn objects
        }
    }

    private void SpawnLoop()
    {
        timeUntilSpawn += Time.deltaTime;

        //spawn after a certain time
        if (timeUntilSpawn >= _spawnTime)
        {
            Spawn();
            timeUntilSpawn = 0;
        }
    }

    private void ClearObjects()
    {
        //destroy all objects in objectParent
        foreach (Transform child in objectParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void Spawn()
    {
        GameObject objectToSpawn = prefabs[Random.Range(0, prefabs.Length)];    //random object is selected

        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity); //selected object is spawned on spawner
        spawnedObject.transform.parent = objectParent;  //inserted into objectParent for clean destruction

        Rigidbody2D objectRB = spawnedObject.GetComponent<Rigidbody2D>();   //get the Rigidbody of the object
        objectRB.velocity = Vector2.left * _speed;  //make them move to the left

        Destroy(spawnedObject, lifeTime);   //destroy object after a certain time, for process power
    }

    private void CalculateFactors()
    {
        _spawnTime = spawnTime / Mathf.Pow(timeAlive, spawnTimeFactor); //shorten the spawntime 
        _speed = speed * Mathf.Pow(timeAlive, speedFactor); //increase the speed
    }

    private void ResetFactors()
    {
        ClearObjects();

        timeAlive = 1f;
        timeUntilSpawn = 0f;
        _spawnTime = spawnTime;
        _speed = speed;
    }
}
