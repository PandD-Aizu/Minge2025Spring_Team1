using System;
using System.Collections.Generic;
using FMODUnity;
using General;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isMoving;                                 // 移動できるか
    private Vector3 goalPosition;　　　　　　　　　　　　　　　// ゴールの方向
    private Vector3 directionUnderY = new (0, -1, 0);      // Y軸の方向
    private Vector3 currentMoveDirection;                  // 現在の移動方向
    private Vector3 prevPosition = Vector3.zero;           // 敵の以前の位置
    private Vector3 prevTargetCellPosition = Vector3.zero; // 敵の以前の移動先のセル位置
    private RaycastHit currentPositionCellHit;             // レイキャストの衝突位置
    private Animator animator;　　　　　　　　　　　　　　　　　// アニメーター
    private GameObject currentPositionCell;                // 現在の位置のセル
    private EnemyStatus enemyStatus;                       // 敵のステータスを管理するクラス
    private EnemyWalkableCell prevEnemyWalkableCell;       // 敵の以前のセル

    private const float MAX_RAYCAST_DISTANCE = 1f;         // レイキャストの最大距離
    
    [Header("Binding")]
    [SerializeField] private LayerMask searchCurrentPositionCellLayerMask;
    
    [Header("SE")]
    [SerializeField] private StudioEventEmitter goalEmitter;
    
    /* プロパティ */
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    
    private void Start()
    {
        // ゴールの位置を取得
        goalPosition = GameManager.Instance.CallRandomGoalPosition();
        
        // 敵のステータスを管理するクラスを取得する
        if (TryGetComponent(out enemyStatus))
            animator = enemyStatus.animator; // アニメーターを取得する
        
        // レイキャストを使用して、現在位置のセルを取得
        Physics.Raycast(transform.position, directionUnderY, out currentPositionCellHit, MAX_RAYCAST_DISTANCE, searchCurrentPositionCellLayerMask);
        
        // レイキャストがコライダーに当たった場合
        if (currentPositionCellHit.collider != null)
        {
            // 歩行可能なセルにいて、移動可能でなくて、敵の状態が移動中なら
            if (currentPositionCellHit.transform.gameObject.TryGetComponent(out EnemyWalkableCell enemyWalkableCell)
                && !isMoving 
                && enemyStatus.enemyState == EnemyStatus.EnemyState.Moving)
           {
                GameManager.Instance.SearchShortestRoot(enemyWalkableCell, goalPosition); // 最短経路を探索
                EnemyMove(enemyWalkableCell);                                             // 移動させる
            }
        }
    }

    private void Update()
    {
        // pause中に敵を止める
        if (Time.timeScale == 0f) return;
        
        Physics.Raycast(transform.position, directionUnderY, out currentPositionCellHit, MAX_RAYCAST_DISTANCE, searchCurrentPositionCellLayerMask);
        Debug.DrawRay(transform.position, directionUnderY, Color.red, 1f);
        if (currentPositionCellHit.collider != null)
        {
            if (currentPositionCellHit.collider.gameObject.tag == GameTagsManager.EnemyGoalPoint)
            {
                Debug.Log("Destroy");
                GameSpawnManager.Instance.EnemyDestroy();
                GameManager.Instance.ClearJudgement();
                enemyStatus.IsDead = true;
                goalEmitter.Play();
                Destroy(this.gameObject);
                GameManager.Instance.ReachedGoal();
                GameManager.Instance.JudgeGameOver();
            }
            else if (currentPositionCellHit.collider.gameObject.tag == GameTagsManager.EnemyWalkable)
            {
                Debug.Log(currentPositionCellHit.transform.gameObject.name);
                if (currentPositionCellHit.transform.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCell) 
                    && !isMoving && enemyStatus.enemyState == EnemyStatus.EnemyState.Moving)
                {
                    if (enemyWalkableCell.NextCell != null)
                    {
                        GameManager.Instance.SearchShortestRoot(enemyWalkableCell, goalPosition);
                        Debug.Log(enemyWalkableCell.NextCell.transform.position);
                    } 
                    EnemyMove(enemyWalkableCell);
                }else if (!(enemyStatus.enemyState == EnemyStatus.EnemyState.Moving))
                {
                    Debug.Log("Stop");
                }
                else
                {
                    Debug.LogError("Not Found EnemyWalkableCell Component : EnemyMovement Update()");
                }
            }
            else
            {
                Debug.LogWarning("No WalkableCell");
            }
        }
        UpdateMoveDirection();
        if (enemyStatus.enemyState == EnemyStatus.EnemyState.Moving)
        {
            animator.SetBool(enemyStatus.movementAnimationParameter, true);
        }
        else
        {
            animator.SetBool(enemyStatus.movementAnimationParameter, false);
        }
    }

    //brief エネミーを移動させる関数
    private void EnemyMove(EnemyWalkableCell enemyWalkableCell)
    {
        Vector3 target;
        if (prevEnemyWalkableCell == null)
        {
            prevEnemyWalkableCell = enemyWalkableCell;
        }
        if (enemyWalkableCell.NextCell != null)
        {
            target = enemyWalkableCell.NextCell.gameObject.transform.position;
            if (prevEnemyWalkableCell != null 
                && enemyWalkableCell.transform.position.y - prevEnemyWalkableCell.transform.position.y >= 0)
            {
                target.y += (enemyWalkableCell.gameObject.transform.position.y -
                             prevEnemyWalkableCell.gameObject.transform.position.y);
                Debug.LogWarning(enemyWalkableCell.transform.position.y - prevEnemyWalkableCell.transform.position.y);
            }
        }
        else
        {
            Debug.Log("goal");
            target = goalPosition;
        }
        target.y += 1;
        float Distance_two = Vector3.Distance(transform.position, target);
        float presentDistance_Location = (enemyStatus.CurrentMoveSpeed * Time.deltaTime) / Distance_two;//移動速度のコントロール
        transform.position = Vector3.Lerp(this.transform.position, target, presentDistance_Location);
        if (prevEnemyWalkableCell != null && enemyWalkableCell == prevEnemyWalkableCell)
        {
            prevEnemyWalkableCell = enemyWalkableCell;
        }
    }

    private void UpdateMoveDirection()
    {
        Vector3 position = transform.position;
        currentMoveDirection = position - prevPosition;
        prevPosition = position;
        enemyStatus.CurrentMoveDirection = currentMoveDirection;
    }
    
}
