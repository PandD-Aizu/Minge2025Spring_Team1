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
    private Vector3 directionUnderY = new Vector3(0, -1, 0);
    private Vector3 currentMoveDirection;
    private Vector3 nextPosition;
    private Vector3 goalSurfacePosition;
    private Animator animator;
    private GameObject currentPositionCell;
    private EnemyStatus enemyStatus;
    private EnemyWalkableCell prevEnemyWalkableCell;
    [SerializeField]private EnemyGoalPointCell goalPointCell;
    private List<PathNode> pathList = new List<PathNode>();
    private Pathfinding pathfinding = new Pathfinding();

    private const float MAX_RAYCAST_DISTANCE = 1f;
    private const float MOVE_SPEED_COEF = 3f;
    private const float DIFFERENCE_WITH_THE_GROUND = 0.5f;
    
    // private const float MOVE_SPEED_COEFFICIENT = 0.01f;　ほんとはだめだけど念のため速度の定数をコメントアウトで残しておく
    
    [Header("Binding")]
    [SerializeField] private LayerMask searchCurrentPositionCellLayerMask;
    
    [Header("SE")]
    [SerializeField] private StudioEventEmitter goalEmitter;
    
    //アクセサ
    private Vector3 GoalPosition {get => goalPointCell.transform.position;}
    
    public bool IsMoving {get => isMoving; set => isMoving = value;}
    private void Start()
    {
        if (this.gameObject.TryGetComponent<EnemyStatus>(out enemyStatus))
        {
               
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
        
        if (nextPosition == this.gameObject.transform.position && IsMoving)
        {
            EnemyMove();
        }

        if (goalSurfacePosition == this.gameObject.transform.position)
        {
            Destroy(gameObject);
        }
    }

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
