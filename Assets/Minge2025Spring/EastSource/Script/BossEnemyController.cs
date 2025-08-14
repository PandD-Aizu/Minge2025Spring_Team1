using System;
using System.Collections.Generic;
using FMODUnity;
using General;
using UnityEngine;
using DG.Tweening;

public class BossEnemyController : MonoBehaviour, IEnemyMovement
{
    public BaseEnemyWalkable startCell;
    public BaseEnemyWalkable distinationCell;
    
    public bool isMoving = true;
    
    private Vector3 nextPosition;//移動先のポジション
    private Vector3 goalSurfacePosition;
    private EnemyStatus enemyStatus;
    private List<PathNode> pathList = new List<PathNode>();
    private Pathfinding pathfinding = new Pathfinding();

    [SerializeField] private List<EnemyWalkableCell> wayPointCells;
    [SerializeField] private EnemyGoalPointCell goalCell;
    
    private const float MOVE_SPEED_COEF = 3f;　//移動速度の係数
    private const float DIFFERENCE_WITH_THE_GROUND = 0.5f;　//ゲームオブジェクトと地面との表面との差
    
    [Header("Binding")]
    [SerializeField] private LayerMask searchCurrentPositionCellLayerMask;
    
    //アクセサ
    public bool IsMoving {get => isMoving; set => isMoving = value;}

    private void Start()
    {
        if (this.gameObject.TryGetComponent<EnemyStatus>(out enemyStatus))
        {
            Debug.Log("can get");
        }
        else
        {
            Debug.LogError("Cannot Get EnemyStatus");
        }
        goalSurfacePosition = goalCell.transform.position;
        goalSurfacePosition.y += 1;
        
        ((IEnemyMovement)this).UpdateRootPath();
        ((IEnemyMovement)this).EnemyMove();
    }
    
    private void Update()
    {
        // pause中に敵を止める
        if (Time.timeScale == 0f) return;
        
        //次のセルまで移動
        if (nextPosition == this.gameObject.transform.position && IsMoving && enemyStatus.enemyState == EnemyStatus.EnemyState.Moving)
        {
            ((IEnemyMovement)this).EnemyMove();
        }
        else if (enemyStatus.enemyState == EnemyStatus.EnemyState.Attacking)
        {
            ((IEnemyMovement)this).StopEnemyMovement();
        }

        //ゴールにたどり着いたら
        if (goalSurfacePosition == this.gameObject.transform.position)
        {
            //ゴールにたどり着いたらGameManagerのGoalReach()を使ってStageLifeUIを更新する
            GameManager.Instance.ReachedGoal();
            Destroy(gameObject);
        }
    }

    void IEnemyMovement.UpdateRootPath()
    {
        if (wayPointCells.Count <= 1)
        {
            nextPosition = goalSurfacePosition;
            this.transform.DOMove(nextPosition, (MOVE_SPEED_COEF / enemyStatus.CurrentMoveSpeed)).SetEase(Ease.Linear);
            return;
        }
        startCell = wayPointCells[0];
        distinationCell = wayPointCells[1];
        pathList = pathfinding.FindPath(startCell, distinationCell);
        foreach (PathNode node in pathList)
        {
            Debug.Log("Root " + ": " + node.NodeId);
        }
        wayPointCells.RemoveAt(0);
    }

    void IEnemyMovement.EnemyMove()
    {
        if (pathList == null) return;
        
        if (pathList.Count <= 0)
        {
            //オブジェクトの表面を計算
            Vector3 distinationPosition = distinationCell.transform.position;
            Vector3 distinationScale = distinationCell.gameObject.transform.localScale;
            distinationPosition.y += (distinationScale.y / 2) + 0.5f;
            nextPosition = distinationPosition;
            this.transform.DOMove(nextPosition, (MOVE_SPEED_COEF / enemyStatus.CurrentMoveSpeed)).SetEase(Ease.Linear);
            ((IEnemyMovement)this).UpdateRootPath();    
            return;
        }
        nextPosition = pathList[0].SurfacePosition;
        nextPosition.y += DIFFERENCE_WITH_THE_GROUND;
        enemyStatus.CurrentMoveDirection = nextPosition - transform.position; // 敵の向きを設定
        pathList.RemoveAt(0);
        this.transform.DOMove(nextPosition, (MOVE_SPEED_COEF / enemyStatus.CurrentMoveSpeed)).SetEase(Ease.Linear);
    }

    void IEnemyMovement.StopEnemyMovement()
    {
        PathNode tempNode = new PathNode("-1", nextPosition);
        pathList.Insert(0, tempNode);
        IsMoving = false;
        transform.DOKill();
    }
}
