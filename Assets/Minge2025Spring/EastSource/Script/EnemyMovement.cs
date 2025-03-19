using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isMoving = false;
    private const float MAX_RAYCAST_DISTANCE = 1f;
    private Vector3 goalPosition;
    private Vector3 directionUnderY = new Vector3(0, -1, 0);
    private GameObject currentPositionCell;
    private RaycastHit currentPositionCellHit;
    private EnemyStatus enemyStatus;
    [Header("Binding")]
    [SerializeField] private LayerMask searchCurrentPositionCellLayerMask;
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
            Debug.LogError("Not Found EnemyStatusComponent");
        }
    }

    private void Update()
    {
        Physics.Raycast(transform.position, directionUnderY, out currentPositionCellHit, MAX_RAYCAST_DISTANCE, searchCurrentPositionCellLayerMask);
        Debug.DrawRay(transform.position, directionUnderY, Color.red, 1f);
        if (currentPositionCellHit.collider != null)
        {
            Debug.Log(currentPositionCellHit.transform.gameObject.name);
            if (currentPositionCellHit.transform.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCell) 
                && !isMoving)
            {
                GameManager.Instance.SearchShortestRoot(enemyWalkableCell, goalPosition);
                Debug.Log(enemyWalkableCell.NextCell.transform.position);
                EnemyMove(enemyWalkableCell);
            }
            else if (isMoving)
            {
                Debug.Log("is moving");
            }
            else
            {
                Debug.LogError("Not Found EnemyWalkableCell Component");
            }
        }
        
        Debug.Log("is is is");
    }

    //brief エネミーを移動させる関数
    private void EnemyMove(EnemyWalkableCell enemyWalkableCell)
    {
        Vector3 target = enemyWalkableCell.NextCell.gameObject.transform.position;
        float Distance_two= Vector3.Distance(transform.position, target);
        target.y = transform.position.y;
        float presentDistance_Location = (Time.time / enemyStatus.CurrentMoveSpeed * 0.01f) / Distance_two;
        transform.position = Vector3.Lerp(this.transform.position, target, presentDistance_Location);
    }
}
