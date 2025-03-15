using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const float MAX_RAYCAST_DISTANCE = 1f;
    private Vector3 goalPosition;
    private Vector3 directionUnderY = new Vector3(0, -1, 0);
    private GameObject currentPositionCell;
    private RaycastHit currentPositionCellHit;
    [Header("Binding")]
    [SerializeField] private LayerMask searchCurrentPositionCellLayerMask;
    private void Start()
    {
        goalPosition = GameManager.Instance.CallRandomGoalPosition();
        Debug.Log(goalPosition);
    }

    private void Update()
    {
        Physics.Raycast(transform.position, directionUnderY, out currentPositionCellHit, MAX_RAYCAST_DISTANCE, searchCurrentPositionCellLayerMask);
        Debug.DrawRay(transform.position, directionUnderY, Color.red, 1f);
        if (currentPositionCellHit.collider != null)
        {
            Debug.Log(currentPositionCellHit.transform.gameObject.name);
            if (currentPositionCellHit.transform.gameObject.TryGetComponent<EnemyWalkableCell>(out EnemyWalkableCell enemyWalkableCell))
            {
                GameManager.Instance.SearchShortestRoot(enemyWalkableCell, goalPosition);
                Debug.Log(enemyWalkableCell.NextCell.transform.position);
            }
        }
    }
}
