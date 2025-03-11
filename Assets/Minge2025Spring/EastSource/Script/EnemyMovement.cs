using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 GoalPosition;
    private GameObject currentPositionCell;
    private void Start()
    {
        GoalPosition = GameManager.Instance.CallRandomGoalPosition();
    }
}
