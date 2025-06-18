using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public event EventHandler OnGameOver;
    public event EventHandler OnGameClear;
    public event EventHandler OnUpdateEnemyWalkableCells;
    
    [Header("Debugger Mode")]
    [SerializeField] private int testWalkableCellNumber = 0;
    [SerializeField] private int maxSpawnEnemies = 10;
    [SerializeField] private int goalCapasity = 5;
    [SerializeField] private bool isdebugMode = false;
    
    private int reachedGoalEnemies = 0;
    private int spawndedEnemies = 0;
    [SerializeField]private GameObject[] enemyWalkableCells;
    private GameObject[] enemyGoalPointCells;
    private List<GameObject> enemySpawnPointCells = new List<GameObject>();
    private List<EnemyWalkableCell> movementRootList;
    //Debug
    [SerializeField]TestObject testObject;
    
    public GameObject[] EnemyWalkableCells {get{return enemyWalkableCells;}}
    public List<EnemyWalkableCell> MovementRootList {get{return movementRootList;}}
    
    private void Awake()
    {
        Instance = this;
        UpdateEnemyWalkableCells();
        UpdateEnemyGoalPointCells();
        UpdateEnemySpawnPointCells();
        
    }

    private void Start()
    {
        reachedGoalEnemies = 0;
        spawndedEnemies = 0;
        testObject.StartPath();
        StageLifeUI.Instance.UpdateStageLifeUI(maxSpawnEnemies, spawndedEnemies, reachedGoalEnemies, goalCapasity);
    }

    //brief シーン上のスポーンポイントを取得する
    //warning 必ずUpdateEnemyWalkableCells()を使用した後に使う
    private void UpdateEnemySpawnPointCells()
    {
        if (enemyWalkableCells.Length <= 0)
        {
            Debug.LogError("Null enemyWalkableCells");
        }
        else
        {
            foreach (GameObject enemyWalkableCell in enemyWalkableCells)
            {
                if (enemyWalkableCell.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCellScript))
                {
                    if (enemyWalkableCellScript.IsSpawnPointCell == true)
                    {
                        enemySpawnPointCells.Add(enemyWalkableCell);
                    }
                }
            }
        }
        Debug.Log(enemySpawnPointCells.Count.ToString());
    }
    
    //brief シーン上のエネミーが通れるエリアを取得する
    public void UpdateEnemyWalkableCells()
    {
        enemyWalkableCells = GameObject.FindGameObjectsWithTag(GameTagsManager.EnemyWalkable);
        foreach (GameObject enemyWalkableCell in enemyWalkableCells)
        {
            Debug.Log("position: "   + enemyWalkableCell.transform.position + "name: " +enemyWalkableCell.gameObject.name);
        }
    }

    public void UpdateEnemyGoalPointCells()
    {
        enemyGoalPointCells = GameObject.FindGameObjectsWithTag(GameTagsManager.EnemyGoalPoint);
        foreach (GameObject enemyGoalPointCell in enemyGoalPointCells)
        {
            Debug.Log("position: " + enemyGoalPointCell.transform.position + "name: " +enemyGoalPointCell.gameObject.name);
        }
        OnUpdateEnemyWalkableCells?.Invoke(this, EventArgs.Empty);
    }

    //brief Enemyがゴールにたどり着くまでの最短距離を計算する

    private void SetMovementRootList()
    {
        this.movementRootList.Reverse();
    }

    public GoalPositionCell CallRandomGoalPosition()
    {
        Random random = new Random();
        return enemyGoalPointCells[random.Next(enemyGoalPointCells.Length)].gameObject.GetComponent<GoalPositionCell>();
    }

    public Vector3 CallRandomSpawnPosition()
    {
        Random random = new Random();
        return enemySpawnPointCells[random.Next(enemySpawnPointCells.Count)].transform.position;
    }
    
    //@brief エネミーがスポーンしたときにカウントする
    public void SpawnEnemy()
    {
        spawndedEnemies++;
        StageLifeUI.Instance.UpdateStageLifeUI(maxSpawnEnemies, spawndedEnemies, reachedGoalEnemies, goalCapasity);
        if (spawndedEnemies >= maxSpawnEnemies)
        {
            GameSpawnManager.Instance.IsPassedSpawnTime = false; //エネミーのスポーンを止める
        }
    }
    
    //@brief すべてのエネミーが出きったかどうか確認の後ゲームクリア判定
    public void ClearJudgement()
    {
        if (spawndedEnemies >= maxSpawnEnemies && GameSpawnManager.Instance.NumberOfEnemies <= 0 )
        {
            OnGameClear?.Invoke(this, EventArgs.Empty);
        }
    }
    
    //@brief エネミーがゴールに到達したときreachedGoalEnemiesを加算し、GoalCapacity以上かどうか確認する
    public void ReachedGoal()
    {
        reachedGoalEnemies++;
        StageLifeUI.Instance.UpdateStageLifeUI(maxSpawnEnemies, spawndedEnemies, reachedGoalEnemies, goalCapasity);
    }

    public void JudgeGameOver()
    {
        if (reachedGoalEnemies >= goalCapasity)
        {
            StageLifeUI.Instance.UpdateStageLifeUI(maxSpawnEnemies, spawndedEnemies, reachedGoalEnemies, goalCapasity);
            //ゲームオーバー
            GameSpawnManager.Instance.IsPassedSpawnTime = false; //エネミーのスポーンを止める
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}