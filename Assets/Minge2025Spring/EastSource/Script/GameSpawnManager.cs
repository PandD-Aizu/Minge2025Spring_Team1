using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameSpawnManager : MonoBehaviour
{
    [Header("Binding")]
    [SerializeField] private GameObject[] enemySpawnOrder;

    public static GameSpawnManager Instance{get; private set;}
    
    private int numberOfEnemies = 0;
    private float nextSpawnTime;
    private float currentCoolTime;
    private bool isSpawning = true;
    private GameObject nextSpawnEnemy;
    private ReadyTimer readyTimer;
    [SerializeField]private int limiterSpawnEnemy = 5;
    [SerializeField]private float readyTime = 10f;
    [SerializeField]private bool isPassedSpawnSpawnTime = true;
    
    public float ReadyTime {get => readyTime;}
    public ReadyTimer ReadyTimer {get => readyTimer; set => readyTimer = value; }
    
    public bool IsPassedSpawnTime { get => isPassedSpawnSpawnTime; set => isPassedSpawnSpawnTime = value; }
    
    private void Awake()
    {
        Instance = this;
        if (enemySpawnOrder.Length <= 0)
        {
            Debug.LogError("Null enemySpawnOrder");
        }
    }

    private void Start()
    {
        currentCoolTime = 0f;
        UpdateNextSpawn();
        IsPassedSpawnTime = false;
        readyTimer = new ReadyTimer();
    }

    private void Update()
    {
        if(readyTimer != null)
        {
            readyTimer.UpdateReadyTime();
        }
        if (isSpawning == false && isPassedSpawnSpawnTime && numberOfEnemies < limiterSpawnEnemy)
        {
            currentCoolTime += Time.deltaTime;
            if (currentCoolTime > nextSpawnTime)
            {
                isSpawning = true;
                SpawnEnemy();
            }
        }
    }

    public void UpdateNextSpawn()
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

    public void SpawnEnemy()
    {
        if (isSpawning)
        {
            Vector3 SpawnPosition = GameManager.Instance.CallRandomSpawnPosition();
            SpawnPosition.y += 1;//このままだとセルに埋まるから
            Instantiate(nextSpawnEnemy, SpawnPosition, Quaternion.identity);
            isSpawning = false; 
            numberOfEnemies++;
            currentCoolTime = 0f;
            UpdateNextSpawn();
        }
    }

    public void EnemyDestroy()
    {
        if (limiterSpawnEnemy <= 0)
        {
            Debug.LogError("Can't use EnemyDestory(): GameSpawnManager.");
        }
        else
        {
            numberOfEnemies--;
        }
    }
}
