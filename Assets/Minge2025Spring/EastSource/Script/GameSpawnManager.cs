using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameSpawnManager : MonoBehaviour
{
    [Header("Binding")]
    [SerializeField] private GameObject[] enemySpawnOrder;

    private GameObject nextSpawnEnemy;
    private float nextSpawnTime;
    private float currentCoolTime;
    private bool isSpawning = true;
    [SerializeField]private bool isPassedSpawnSpawnTime = true;
    
    public bool IsPassedSpawnTime { get => isPassedSpawnSpawnTime; set => isPassedSpawnSpawnTime = value; }
    
    private void Awake()
    {
        if (enemySpawnOrder.Length <= 0)
        {
            Debug.LogError("Null enemySpawnOrder");
        }
    }

    private void Start()
    {
        currentCoolTime = 0f;
        UpdateNextSpawn();
        SpawnEnemy();
        UpdateNextSpawn();
        IsPassedSpawnTime = true;
    }

    private void Update()
    {
        if (isSpawning == false && isPassedSpawnSpawnTime)
        {
            currentCoolTime += Time.deltaTime;
            if (currentCoolTime > nextSpawnTime)
            {
                isSpawning = true;
                currentCoolTime = 0f;
                SpawnEnemy();
            }
        }
    }

    private void UpdateNextSpawn()
    {
        Random random = new Random();
        int index = random.Next(enemySpawnOrder.Length);
        if (enemySpawnOrder[index].TryGetComponent<EnemyStatus>(out EnemyStatus enemyStatus))
        {
            nextSpawnEnemy = enemySpawnOrder[index];
            nextSpawnTime = enemyStatus.SpawnCooltime;
        }
        else
        {
            Debug.LogError("Not Found EnemyStatus in UpdateNextSpawn()");
        }
    }

    private void SpawnEnemy()
    {
        if (isSpawning)
        {
            Vector3 SpawnPosition = GameManager.Instance.CallRandomSpawnPosition();
            SpawnPosition.y += 1;//このままだとセルに埋まるから
            Instantiate(nextSpawnEnemy, SpawnPosition, Quaternion.identity);
            isSpawning = false;
            UpdateNextSpawn();
        }
    }
}
