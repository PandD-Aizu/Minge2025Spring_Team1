using System;
using System.Collections.Generic;
using FMODUnity;
using General;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    public EnemyWalkableCell startCell;
    
    private bool isMoving = true;
    private RaycastHit currentPositionCellHit;
    private Vector3 directionUnderY = new Vector3(0, -1, 0);//足元の方向にRayを飛ばすための単位ベクトル
    private Vector3 nextPosition;//移動先のポジション
    private Vector3 goalSurfacePosition;
    private Animator animator;
    private EnemyStatus enemyStatus;
    private EnemyGoalPointCell goalPointCell;
    private List<PathNode> pathList = new List<PathNode>();
    private Pathfinding pathfinding = new Pathfinding();

    private const float MAX_RAYCAST_DISTANCE = 1f;
    private const float MOVE_SPEED_COEF = 3f;　//移動速度の係数
    private const float DIFFERENCE_WITH_THE_GROUND = 0.5f;　//ゲームオブジェクトと地面との表面との差
    
    [Header("Binding")]
    [SerializeField] private LayerMask searchCurrentPositionCellLayerMask;
    
    [Header("SE")]
    [SerializeField] private StudioEventEmitter goalEmitter;
    
    //アクセサ
    private Vector3 GoalPosition {get => goalPointCell.transform.position;}
    
    public bool IsMoving {get => isMoving; set => isMoving = value;}
    
    //EnemyStatusの初期化、goalPoint、pathListを取得
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
        goalPointCell = GameManager.Instance.CallRandomEnemyGoalPoint();
        goalSurfacePosition = GoalPosition;
        goalSurfacePosition.y += 1;
        UpdateRootPath();
        EnemyMove();
    }

    private void Update()
    {
        // pause中に敵を止める
        if (Time.timeScale == 0f) return;
        
        //次のセルまで移動
        if (nextPosition == this.gameObject.transform.position && IsMoving)
        {
            EnemyMove();
        }

        //ゴールにたどり着いたら
        if (goalSurfacePosition == this.gameObject.transform.position)
        {
            Destroy(gameObject);
        }
    }

    //pathListを新しく計算しなおす関数
    private void UpdateRootPath()
    {
        Physics.Raycast(transform.position, directionUnderY, out currentPositionCellHit, MAX_RAYCAST_DISTANCE, searchCurrentPositionCellLayerMask);
        Debug.DrawRay(transform.position, directionUnderY, Color.red, 1f);
        if (currentPositionCellHit.collider.gameObject != null)
        {
            Debug.Log("not null");
            startCell = currentPositionCellHit.collider.gameObject.GetComponent<EnemyWalkableCell>();
        }
        pathList = pathfinding.FindPath(startCell, goalPointCell);
        foreach (PathNode node in pathList)
        {
            Debug.Log("Root " + ": " + node.NodeId);
        }
    }

    //brief エネミーを移動させる関数
    private void EnemyMove()
    {
        if (pathList == null) return;

        if (pathList.Count <= 0)
        {
            nextPosition = goalSurfacePosition;
            this.transform.DOMove(nextPosition, (MOVE_SPEED_COEF / enemyStatus.CurrentMoveSpeed)).SetEase(Ease.Linear);
            return;
        }

        nextPosition = pathList[0].SurfacePosition;
        nextPosition.y += DIFFERENCE_WITH_THE_GROUND;
        pathList.RemoveAt(0);
        this.transform.DOMove(nextPosition, (MOVE_SPEED_COEF / enemyStatus.CurrentMoveSpeed)).SetEase(Ease.Linear);
    }
    
    
}
