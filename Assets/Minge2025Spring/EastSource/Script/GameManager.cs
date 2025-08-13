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
    
    [SerializeField]private List<GameObject>enemyWalkableCells;
    private GameObject[] enemyGoalPointCells;
    private GameObject[] characterPlacementCells;
    private List<GameObject> enemySpawnPointCells = new List<GameObject>();
    private List<EnemyWalkableCell> movementRootList;
    //Debug
    [SerializeField]TestObject testObject;
    
    public List<GameObject> EnemyWalkableCells {get{return enemyWalkableCells;}}
    public List<EnemyWalkableCell> MovementRootList {get{return movementRootList;}}
    
    public int MaxSpawnEnemies { get => maxSpawnEnemies; }
    public int GoalCapasity { get => goalCapasity; }
    private void Awake()
    {
        Instance = this;
        UpdateEnemyWalkableCells();
        UpdateEnemyGoalPointCells();
        UpdateEnemySpawnPointCells();
        
    }

    //brief シーン上のスポーンポイントを取得する
    //warning 必ずUpdateEnemyWalkableCells()を使用した後に使う
    private void UpdateEnemySpawnPointCells()
    {
        if (enemyWalkableCells.Count <= 0)
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
        enemyWalkableCells = new List<GameObject>(GameObject.FindGameObjectsWithTag(GameTagsManager.EnemyWalkable));
        GameObject[] tempCharacterPlacementCells = GameObject.FindGameObjectsWithTag(GameTagsManager.CharacterPlacement);
        foreach (GameObject tempCharacterPlacementCell in tempCharacterPlacementCells)
        {
            if (tempCharacterPlacementCell.gameObject.TryGetComponent<BaseEnemyWalkable>(out BaseEnemyWalkable baseEnemyWalkable))
            {
                enemyWalkableCells.Add(tempCharacterPlacementCell);
            }

        }
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

    public EnemyGoalPointCell CallRandomEnemyGoalPoint()
    {
        Random random = new Random();
        Debug.LogWarning(enemyGoalPointCells[random.Next(enemyGoalPointCells.Length)].gameObject.GetComponent<EnemyGoalPointCell>().name);
        return enemyGoalPointCells[random.Next(enemyGoalPointCells.Length)].gameObject.GetComponent<EnemyGoalPointCell>();
    }

    public EnemyWalkableCell CallRandomSpawnPositionCell()
    {
        Random random = new Random();
        return enemySpawnPointCells[random.Next(enemySpawnPointCells.Count)].GetComponent<EnemyWalkableCell>();
    }
    
    //@brief エネミーがスポーンしたときにカウントする
    public void SpawnEnemy()
    {
        StageLifeUIController.Instance.UpdateSpawnedEnemies(1);
        if (StageLifeUIController.Instance.SpawnedEnemies >= maxSpawnEnemies)
        {
            GameSpawnManager.Instance.IsPassedSpawnTime = false; //エネミーのスポーンを止める
        }
    }
    
    //@brief すべてのエネミーが出きったかどうか確認の後ゲームクリア判定
    public void ClearJudgement()
    {
        if (StageLifeUIController.Instance.SpawnedEnemies >= maxSpawnEnemies && GameSpawnManager.Instance.NumberOfEnemies <= 0 )
        {
            OnGameClear?.Invoke(this, EventArgs.Empty);
        }
    }
    
    //@brief エネミーがゴールに到達したときreachedGoalEnemiesを加算し、GoalCapacity以上かどうか確認する
    public void ReachedGoal()
    {
        StageLifeUIController.Instance.UpdateReachGoalEnemies(1);
        JudgeGameOver();
    }

    public void JudgeGameOver()
    {
        if (StageLifeUIController.Instance.MaxSpawnEnemies >= goalCapasity)
        {
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