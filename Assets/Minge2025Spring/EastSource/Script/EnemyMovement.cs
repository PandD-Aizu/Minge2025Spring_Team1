using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isMoving = false;
    private const float MAX_RAYCAST_DISTANCE = 1f;
    private Vector3 goalPosition;
    private Vector3 directionUnderY = new Vector3(0, -1, 0);
    private Vector3 currentMoveDirection;
    private Vector3 prevPosition = Vector3.zero;
    private GameObject currentPositionCell;
    private RaycastHit currentPositionCellHit;
    private EnemyStatus enemyStatus;
    private const float MOVE_SPEED_COEFFICIENT = 0.01f;
    [Header("Binding")]
    [SerializeField] private LayerMask searchCurrentPositionCellLayerMask;
    
    public bool IsMoving {get => isMoving; set => isMoving = value;}
    private void Start()
    {
        goalPosition = GameManager.Instance.CallRandomGoalPosition();
        Debug.Log(goalPosition);
        if (TryGetComponent<EnemyStatus>(out enemyStatus))
        {
            Debug.Log(enemyStatus);
        }
        else
        {
            Debug.LogError("Not Found EnemyStatusComponent : EnemyComponent Start()");
        }
        Physics.Raycast(transform.position, directionUnderY, out currentPositionCellHit, MAX_RAYCAST_DISTANCE, searchCurrentPositionCellLayerMask);
        if (currentPositionCellHit.collider != null)
        {
            Debug.Log("Start: " + currentPositionCellHit.transform.gameObject.name);
            if (currentPositionCellHit.transform.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCell)
                && !isMoving && enemyStatus.enemyState == EnemyStatus.EnemyState.Moving)
            {
                GameManager.Instance.SearchShortestRoot(enemyWalkableCell, goalPosition);
                if (enemyWalkableCell.NextCell != null)
                {
                    Debug.Log(enemyWalkableCell.NextCell.transform.position);
                }
                EnemyMove(enemyWalkableCell);
            }
        }
    }

    private void Update()
    {
        Physics.Raycast(transform.position, directionUnderY, out currentPositionCellHit, MAX_RAYCAST_DISTANCE, searchCurrentPositionCellLayerMask);
        Debug.DrawRay(transform.position, directionUnderY, Color.red, 1f);
        if (currentPositionCellHit.collider != null)
        {
            if (currentPositionCellHit.collider.gameObject.tag == GameTagsManager.EnemyGoalPoint)
            {
                Debug.Log("Destroy");
                Destroy(this.gameObject);
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
    }

    //brief エネミーを移動させる関数
    private void EnemyMove(EnemyWalkableCell enemyWalkableCell)
    {
        Vector3 target;
        if (enemyWalkableCell.NextCell != null)
        {
            target = enemyWalkableCell.NextCell.gameObject.transform.position;
        }
        else
        {
            Debug.Log("goal");
            target = goalPosition;
        }
        float Distance_two = Vector3.Distance(transform.position, target);
        target.y = transform.position.y;
        float presentDistance_Location = (enemyStatus.CurrentMoveSpeed * MOVE_SPEED_COEFFICIENT) / Distance_two;
        transform.position = Vector3.Lerp(this.transform.position, target, presentDistance_Location);
    }

    private void UpdateMoveDirection()
    {
        Vector3 position = transform.position;
        currentMoveDirection = position - prevPosition;
        prevPosition = position;
        enemyStatus.CurrentMoveDirection = currentMoveDirection;
    }
    
}
